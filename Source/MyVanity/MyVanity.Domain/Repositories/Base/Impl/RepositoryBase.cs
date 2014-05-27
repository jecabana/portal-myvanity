using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyVanity.Domain.Repositories.Base.Impl
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ModelContainer _context;
        private readonly DbSet<TEntity> _dbSet;  

        public RepositoryBase(ModelContainer context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet;
        }

        public virtual TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public Task<TEntity> FindByIdAsync(int id)
        {
            return _dbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Update(TEntity entity)
        {
            var existent = GetOne(entity.Id);

            var oldEntry = _context.Entry(existent);
            UpdateForeignKeyProperties(existent, entity);
            oldEntry.CurrentValues.SetValues(entity);
            oldEntry.State = EntityState.Modified;
        }

        public TEntity GetOne(long id)
        {
            return _dbSet.Find(id);
        }

        private ObjectContext ObjectContext
        {
            get
            {
                var wrapper = (IObjectContextAdapter)_context;
                return wrapper.ObjectContext;
            }
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, 
                                        Func<IQueryable<TEntity>, 
                                        IOrderedQueryable<TEntity>> orderBy = null, 
                                        string includeProperties = "")
        {
            IQueryable <TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var property in includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                query.Include(property);

            if (orderBy != null)
                return orderBy(query).ToList();
            
            return query.ToList();
        }

        private void UpdateForeignKeyProperties(TEntity sourceEntity, TEntity newEntity)
        {
            var entityType = typeof(TEntity);
            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                if (!property.PropertyType.IsGenericType)
                {
                    if (!typeof(IEntity).IsAssignableFrom(property.PropertyType))
                        continue;

                    var propSourceEntity = property.GetValue(sourceEntity, new object[] { });
                    var propDestEntity = property.GetValue(newEntity, new object[] { });

                    var sEntity = propSourceEntity as IEntity;
                    var dEntity = propDestEntity as IEntity;

                    if (sEntity != null && dEntity != null)
                    {
                        if (sEntity.Id == dEntity.Id)
                            continue;

                        property.SetValue(sourceEntity, propDestEntity, new object[] { });
                    }
                    else if (sEntity != null)
                        property.SetValue(sourceEntity, propDestEntity, new object[] { });
                }
                else
                {
                    var genericType = property.PropertyType.GetGenericTypeDefinition();

                    if (genericType != typeof(ICollection<>))
                        continue;

                    var genericArguments = property.PropertyType.GetGenericArguments();
                    var collectionType = genericArguments[0];

                    if (!typeof(IEntity).IsAssignableFrom(collectionType))
                        continue;

                    var newProp = (IEnumerable<IEntity>)property.GetValue(newEntity, new object[] { });
                    UpdateForeignCollection(newProp, property.Name , collectionType, sourceEntity, entityType);
                }
            }
        }

        private void UpdateForeignCollection(IEnumerable<IEntity> newProp, string propName, Type listType, TEntity sourceEntity, Type entityType)
        {
            if (newProp == null)
                return;

            var queryableListDbSet = (IQueryable<object>)_context.Set(listType);
            var filter = CreateFilterForNavigationProperty(listType, sourceEntity.Id);

            if (filter != null)
            {
                var existingItems = queryableListDbSet.Where(filter).ToList();

                foreach (var entity in existingItems)
                {
                    var entry = _context.Entry(entity);
                    var boxed = (IEntity)entity;
                    var shouldDelete = newProp.All(x => x.Id != boxed.Id);

                    if (shouldDelete)
                        entry.State = EntityState.Deleted;
                    else
                    {
                        entry.State = boxed.Id == 0 ? EntityState.Added : EntityState.Modified;

                        if (entry.State == EntityState.Added)
                            throw new InvalidOperationException("Cannot add an item from the source entity list");
                    }
                }
            }
            else
            {
                //Handle Many to Many
                var existing = GetOne(sourceEntity.Id);

                if (existing != null)
                {
                    var oldCollection = (IEnumerable<IEntity>)existing.GetType().GetProperty(propName).GetValue(existing);

                    var mustInsert = newProp.Except(oldCollection);

                    foreach (var toInsert in mustInsert.ToList())
                    {
                        ObjectContext.ObjectStateManager.ChangeRelationshipState(sourceEntity, toInsert, propName,
                                EntityState.Added);
                    }

                    var mustDelete = oldCollection.Except(newProp);

                    foreach (var toDelete in mustDelete.ToList())
                    {
                        ObjectContext.ObjectStateManager.ChangeRelationshipState(sourceEntity, toDelete, propName,
                                EntityState.Deleted);
                    }
                }
            }

            foreach (IEntity newItem in newProp)
            {
                var entityNavPropertyId = entityType.Name + "Id";
                var entityNavigationProperty = listType.GetProperties().SingleOrDefault(prop => prop.Name == entityType.Name);
                var entityNavProperty = listType.GetProperties().SingleOrDefault(prop => prop.Name == entityNavPropertyId);


                if (entityNavigationProperty != null)
                {
                    entityNavigationProperty.SetValue(newItem, sourceEntity, new object[] { });
                    //disconectedGraph.Add(entityNavigationProperty.Name, sourceEntity);
                }

                if (entityNavProperty != null)
                {
                    entityNavProperty.SetValue(newItem, sourceEntity.Id, new object[] { });
                    //disconectedGraph.Add(entityNavProperty.Name, sourceEntity.Id);
                }

                //CleanEntity(newItem, disconectedGraph);

                if (newItem.Id == 0)
                {
                    var existingDbEntry = _context.Entry(newItem);
                    existingDbEntry.State = EntityState.Added;
                }
            }
        }

        private Expression<Func<object, bool>> CreateFilterForNavigationProperty(Type childType, int id)
        {
            var propertyName = typeof(TEntity).Name;
            var propertyExists = childType.GetProperties().SingleOrDefault(x => x.Name == propertyName);

            if (propertyExists == null)
                return null;

            var param = Expression.Parameter(typeof(object), "p");
            var boxing = Expression.TypeAs(param, childType);

            var member = Expression.Property(boxing, propertyName);
            var memberId = Expression.PropertyOrField(member, "Id");

            var idExpression = Expression.Constant(id);
            var comparison = Expression.Equal(memberId, idExpression);

            return Expression.Lambda<Func<object, bool>>(comparison, param);
        }

        public static IList BuildListOfType(Type tp)
        {
            var genericList = typeof(List<>);
            var type = genericList.MakeGenericType(tp);

            return (IList)Activator.CreateInstance(type);
        }
    }

    public static class Extensions
    {
        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other,
                                                                                Func<T, TKey> getKey)
        {
            return from item in items
                   join otherItem in other on getKey(item)
                   equals getKey(otherItem) into tempItems
                   from temp in tempItems.DefaultIfEmpty()
                   where ReferenceEquals(null, temp) || temp.Equals(default(T))
                   select item;

        }
    }
}

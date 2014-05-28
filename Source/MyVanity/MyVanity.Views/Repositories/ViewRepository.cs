using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyVanity.Common;
using MyVanity.Common.Helpers;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.Results;
using MyVanity.Views.Filters;

namespace MyVanity.Views.Repositories
{
    public class ViewRepository<TEntity, TModel> : IViewRepository<TModel> where TModel : ModelBase where TEntity : class, IEntity
    {
        private readonly IModelConverter<TEntity, TModel> _modelConverter;
        protected readonly IUnitOfWork UnitOfWork;

        public ViewRepository(IModelConverter<TEntity, TModel> modelConverter, IUnitOfWork unitOfWork)
        {
            _modelConverter = modelConverter;
            UnitOfWork = unitOfWork;
        }

         public virtual PagedResult<IEnumerable<TModel>> Get(FilterInformation info)
        {
            var tFilter = new TypedFilter<TEntity>(info.PageSize, info.Page)
            {
                OrderColumn = info.OrderColumn,
                SortMode = info.SortMode
            };

            var result = Filter(tFilter);
            
             return new PagedResult<IEnumerable<TModel>>(result.PageSize, result.PageNumber, result.TotalItems ,result.TotalPages)
                   {
                       Result = result.Result.Select(x => _modelConverter.ConvertToModel(x)),
                       SortColumn = result.SortColumn,
                       SortOrder = result.SortOrder
                   };
        }

        protected PagedResult<IEnumerable<TModel>> FilterModel(TypedFilter<TEntity> filter)
        {
            var filtered = Filter(filter);

            return new PagedResult<IEnumerable<TModel>>(filtered.PageSize, filtered.PageNumber, filtered.TotalItems, filtered.TotalPages)
                   {
                       Result = filtered.Result.Select(x => _modelConverter.ConvertToModel(x))
                   };
        }

        private PagedResult<IEnumerable<TEntity>> Filter(TypedFilter<TEntity> filter)
        {
            var repository = UnitOfWork.GetRepository<TEntity>();

            var pageSize = filter.PageSize;
            var pageNumber = filter.Page;
            var query = repository.Query();

            var total = filter.Filter == null ? query.Count() : query.Count(filter.Filter);

            pageSize = pageSize <= 0 ? total : pageSize;

            if (total == 0)
                return new PagedResult<IEnumerable<TEntity>>(pageSize, pageNumber, 0, 0)
                {
                    Result = new List<TEntity>()
                };

            var totalPages = total == pageSize ? 0 : total / pageSize;

            if (filter.Filter != null)
                query = repository.Query().Where(filter.Filter);
            else
                query = repository.Query();

            filter.Page = pageNumber;
            filter.PageSize = pageSize;

            var queriedItems = SortBy(query, filter);

            var result = new PagedResult<IEnumerable<TEntity>>(pageSize, pageNumber, total, totalPages)
            {
                Result = queriedItems.ToList(),
                SortColumn = filter.OrderColumn,
                SortOrder = filter.SortMode
            };

            return result;
        }

        private IEnumerable<TEntity> SortBy(IQueryable<TEntity> source, FilterInformation filterInformation)
        {
            var column = filterInformation.OrderColumn;
            var sortMode = filterInformation.SortMode;
            var skip = filterInformation.Page * filterInformation.PageSize;

            var property = ReflectionExtensions.GetProperty(typeof(TEntity), column);

            if (property == null)
            {
                var sorted = sortMode == SortMode.Ascending ?
                    source.OrderBy(e => e.Id) :
                    source.OrderByDescending(e => e.Id);

                return sorted.Skip(skip).Take(filterInformation.PageSize);
            }

            var multipleChildren = column.Split('.').Count() > 1;

            if (property.PropertyType == typeof(DateTime))
            {
                var filter = multipleChildren ? ReflectionExtensions.GetExpression<TEntity, DateTime>(column) :
                                                ReflectionExtensions.GetPropertyAccessorWithType<TEntity, DateTime>(column);

                source = sortMode == SortMode.Ascending
                             ? source.OrderBy(filter)
                             : source.OrderByDescending(filter);
            }
            else
            {
                var filter = multipleChildren ? ReflectionExtensions.GetExpression<TEntity, object>(column) :
                                                ReflectionExtensions.GetPropertyAccessorWithType<TEntity, object>(column);

                source = sortMode == SortMode.Ascending
                             ? source.OrderBy(filter)
                             : source.OrderByDescending(filter);
            }

            return source.Skip(skip).Take(filterInformation.PageSize);
        }        

        
        public virtual IEnumerable<TModel> GetAll(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>,
                                 IOrderedQueryable<TModel>> orderBy = null,
                                 string includeProperties = "")
        {
            var repository = UnitOfWork.GetRepository<TEntity>().Get();
            return repository.Select(entity => _modelConverter.ConvertToModel(entity));
        }

        
        public virtual void Insert(TModel model)
        {
            var entity = _modelConverter.ConvertToSource(model);
            UnitOfWork.GetRepository<TEntity>().Insert(entity);
            UnitOfWork.SaveChanges();
        }

        
        public virtual void Update(TModel model)
        {
            var entity = _modelConverter.ConvertToSource(model);
            UnitOfWork.GetRepository<TEntity>().Update(entity);
            UnitOfWork.SaveChanges();
        }

        
        public virtual TModel FindById(int id)
        {
            var entity = UnitOfWork.GetRepository<TEntity>().FindById(id);
            var model = _modelConverter.ConvertToModel(entity);

            return model;
        }

        
        public virtual async Task<TModel> FindAsync(int id)
        {
            var entity = await UnitOfWork.GetRepository<TEntity>().FindByIdAsync(id);
            var model = _modelConverter.ConvertToModel(entity);

            return model;
        }

        
        public virtual void Delete(TModel model)
        {
            Delete(model.Id);
        }

        
        public virtual void Delete(int id)
        {
            var repository = UnitOfWork.GetRepository<TEntity>();
            repository.Delete(id);

            UnitOfWork.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyVanity.Common.Autofac;

namespace MyVanity.Domain.Repositories.Base
{
    public interface IRepository<T> : IPerRequestDependency
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, 
                                 IOrderedQueryable<T>> orderBy = null, 
                                 string includeProperties = "");

        T FindById(int id);

        Task<T> FindByIdAsync(int id);

        IQueryable<T> Query(); 

        void Insert(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Update(T entity);
    }
}

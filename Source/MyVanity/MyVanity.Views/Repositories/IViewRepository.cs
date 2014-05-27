using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyVanity.Common.Autofac;
using MyVanity.Model;
using MyVanity.Model.Results;
using MyVanity.Views.Filters;

namespace MyVanity.Views.Repositories
{
    public interface IViewRepository<TModel> : IPerRequestDependency where TModel : ModelBase
    {
        PagedResult<IEnumerable<TModel>> Get(FilterInformation filter);

        /// <summary>
        /// This is deprecated and should be removed
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<TModel> GetAll(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>,
                                 IOrderedQueryable<TModel>> orderBy = null,
                                 string includeProperties = "");

        void Insert(TModel model);

        void Update(TModel model);

        TModel FindById(int id);

        Task<TModel> FindAsync(int id);

        void Delete(TModel model);

        void Delete(int id);
       
    }
}

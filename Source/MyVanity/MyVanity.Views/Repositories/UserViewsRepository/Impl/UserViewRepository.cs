using System.Threading.Tasks;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;

namespace MyVanity.Views.Repositories.UserViewsRepository
{
    public class UserViewRepository<TEntity, TModel> : ViewRepository<TEntity,TModel>, IUserViewRepository<TModel> where TEntity : User 
                                                                                   where TModel  : ModelBase
    {
        protected UserViewRepository(IModelConverter<TEntity, TModel> modelConverter, IUnitOfWork unitOfWork)
            : base(modelConverter, unitOfWork)
        { }

        public Task<LoggedUserViewModel> GetLoggedViewAsync(int userId)
        {
            return
                UnitOfWork.GetRepository<TEntity>()
                    .FindByIdAsync(userId)
                    .ContinueWith(
                        t => new LoggedUserViewModel
                                 {
                                     Id = t.Result.Id,
                                     UserName = t.Result.UserName
                                 });
        }

    }
}

using System.Threading.Tasks;
using MyVanity.Model;

namespace MyVanity.Views.Repositories.UserViewsRepository
{
    public interface IUserViewRepository<TModel> : IViewRepository<TModel> where TModel : ModelBase
    {
        Task<LoggedUserViewModel> GetLoggedViewAsync(int userId);
    }
}
using MyVanity.Domain.Repositories.Base;

namespace MyVanity.Domain.Repositories.UsersRepository
{
    public interface IUserRepository<T> : IRepository<T> where T : User
    {
        T FindByName(string userName); 
    }
}

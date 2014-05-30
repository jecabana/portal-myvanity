using System.Linq;
using MyVanity.Domain.Repositories.Base.Impl;

namespace MyVanity.Domain.Repositories.UsersRepository.Impl
{
    public class UsersRepository<T> : RepositoryBase<T>, IUserRepository<T> where T : User
    {
        public UsersRepository(ModelContainer context) : base(context)
        {
            
        }

        public T FindByName(string userName)
        {
            return Get(x => x.UserName == userName).SingleOrDefault();
        }
    }
}

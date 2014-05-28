using System.Threading.Tasks;
using MyVanity.Common.Autofac;
using MyVanity.Domain.Repositories.Base;
using MyVanity.Domain.Repositories.UsersRepository;

namespace MyVanity.Domain.UoW
{
    public interface IUnitOfWork : IPerRequestDependency 
    {
        void SaveChanges();

        Task<int> SaveChangesAsync();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

        IUserRepository<TEntity> GetUserRepository<TEntity>() where TEntity : User;
    }
}

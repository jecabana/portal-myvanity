using System.Threading.Tasks;
using MyVanity.Domain.Repositories.Base;
using MyVanity.Domain.Repositories.UsersRepository;

namespace MyVanity.Domain.UoW.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModelContainer _context;

        public UnitOfWork(ModelContainer context)
        {
            _context = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            return Common.Autofac.Helpers.PerHttpSafeResolve<IRepository<TEntity>>().Invoke();
        }

        public IUserRepository<TEntity> GetUserRepository<TEntity>() where TEntity : User
        {
            return GetRepository<TEntity>() as IUserRepository<TEntity>;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

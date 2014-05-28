using MyVanity.Domain.Repositories.UsersRepository.Impl;

namespace MyVanity.Domain.Repositories.AdminsRepository.Impl
{
    public class AdminRepository : UsersRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ModelContainer context) : base(context)
        {}
    }
}

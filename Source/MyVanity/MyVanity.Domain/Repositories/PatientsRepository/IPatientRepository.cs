using MyVanity.Domain.Repositories.UsersRepository;

namespace MyVanity.Domain.Repositories.PatientsRepository
{
    public interface IPatientRepository : IUserRepository<Patient>
    {
        
    }
}

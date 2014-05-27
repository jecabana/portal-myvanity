using Autofac;
using MyVanity.Domain.Repositories.AgentsRepository.Impl;
using MyVanity.Domain.Repositories.Base;
using MyVanity.Domain.Repositories.PatientsRepository.Impl;
using MyVanity.Domain.Repositories.UsersRepository;

namespace MyVanity.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AgentsRepository>().As<IUserRepository<Agent>>();
            builder.RegisterType<PatientRepository>().As<IUserRepository<Patient>>();
            builder.RegisterType<IUserRepository<Admin>>().As<IRepository<Admin>>();
        }
    }
}

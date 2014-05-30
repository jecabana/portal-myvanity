using MyVanity.Domain.Repositories.UsersRepository.Impl;

namespace MyVanity.Domain.Repositories.AgentsRepository.Impl
{
    public class AgentsRepository : UsersRepository<Agent>, IAgentRepository
    {

        public AgentsRepository(ModelContainer context) : base(context) {   }
    }
}

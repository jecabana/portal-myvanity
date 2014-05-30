using System.Collections.Generic;
using System.Linq;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.AgentModels.Impl;
using MyVanity.Services.Membership;
using MyVanity.Views.Repositories.UserViewsRepository;

namespace MyVanity.Views.Repositories.AgentViewsRepository.Impl
{
    public class AgentViewRepository : UserViewRepository<Agent, AgentEditModel>, IAgentViewRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModelConverter<Agent, AgentEditModel> _modelConverter; 

        public AgentViewRepository(IModelConverter<Agent, AgentEditModel> modelConverter, 
                                   IUnitOfWork unitOfWork) : base(modelConverter, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _modelConverter = modelConverter;
        }

        public ApplicationRole MapTypeToRole(AgentType type)
        {
            switch(type)
            {
                case AgentType.FinanceCoordinator:
                    return ApplicationRole.FinanceCoordinator;
                
                case AgentType.SurgicalCoordinator:
                    return ApplicationRole.SurgicalCoordinator;
                
                case AgentType.HomeAwayAssistant:
                    return ApplicationRole.HomeAwayAssistant;   
                
                case AgentType.MedicalAssistant:
                    return ApplicationRole.MedicalAssistant;
               
                default:
                    return ApplicationRole.SurgicalCoordinator;
            }
        }

        public List<AgentEditModel> FilterAgents(int? distinctFrom, AgentType? type)
        {
            var repository = _unitOfWork.GetRepository<Agent>();

            List<Agent> agents;

            if (distinctFrom != null && type != null)
                agents = repository.Get(x => x.Id != distinctFrom.Value && x.Type == type.Value).ToList();
            else if (distinctFrom != null)
                agents = repository.Get(x => x.Id != distinctFrom.Value).ToList();
            else if (type != null)
                agents = repository.Get(x => x.Type == type.Value).ToList();
            else
                agents = repository.Get().ToList();

            return agents.Select(x => _modelConverter.ConvertToModel(x)).ToList();
        }
    }
}

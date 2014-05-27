
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.ProfileModels.Profile;

namespace MyVanity.Model.AgentModels.Impl
{
    public class AgentModelConverter : IAgentModelConverter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProfileModelConverter _profileModelConverter;

        public AgentModelConverter(IUnitOfWork unitOfWork, IProfileModelConverter profileModelConverter)
        {
            _unitOfWork = unitOfWork;
            _profileModelConverter = profileModelConverter;
        }

        public AgentEditModel ConvertToModel(Agent entity)
        {
            var model = ModelConverterHelper.CopyObjectProperties(entity, new AgentEditModel());
            model.ProfileDetails = _profileModelConverter.ConvertToModel(entity.PersonDetails);
            
            return model;
        }

        public Agent ConvertToSource(AgentEditModel model)
        {
            var agent = model.Id != 0 ? _unitOfWork.GetRepository<Agent>().FindById(model.Id) 
                                        : new Agent();

            var profile = _profileModelConverter.ConvertToSource(model.ProfileDetails);
            agent.PersonDetails = profile;
            agent = ModelConverterHelper.CopyObjectProperties(model, agent);

            return agent;
        }
    }
}

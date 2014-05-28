using System.Linq;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.ProfileModels.Contact;
using MyVanity.Model.ProfileModels.Profile;

namespace MyVanity.Model.PatientModels.Impl
{
    public class PatientModelConverter : IPatientModelConverter
    {
        private readonly IContactModelConverter _contactModelConverter;
        private readonly IProfileModelConverter _profileModelConverter;
        private readonly IUnitOfWork _unitOfWork;

        public PatientModelConverter(IContactModelConverter contactModelConverter, IProfileModelConverter profileModelConverter, IUnitOfWork unitOfWork)
        {
            _contactModelConverter = contactModelConverter;
            _profileModelConverter = profileModelConverter;
            _unitOfWork = unitOfWork;
        }

        public PatientEditModel ConvertToModel(Patient entity)
        {
            var contactModel = _contactModelConverter.ConvertToModel(entity.Contact);
            var profileModel = _profileModelConverter.ConvertToModel(entity.Profile);

            var patientModel = new PatientEditModel { 
                Id = entity.Id, 
                Contact = contactModel, 
                Profile = profileModel, 
                CurrentAgent = entity.Agent.UserName,
                AgentId = entity.AgentId,
                AgentName = entity.Agent.PersonDetails.FullName
            };

            patientModel = ModelConverterHelper.CopyObjectProperties(entity, patientModel);

            return patientModel;
        }

        public Patient ConvertToSource(PatientEditModel model)
        {
            Patient existingPatient;
            if (model.Id != 0)
            {
                var patientRepository = _unitOfWork.GetRepository<Patient>();
                existingPatient = patientRepository.FindById(model.Id);
            }
            else
                existingPatient = new Patient();

            existingPatient = ModelConverterHelper.CopyObjectProperties(model, existingPatient);

            var contact = _contactModelConverter.ConvertToSource(model.Contact);
            var profile = _profileModelConverter.ConvertToSource(model.Profile);

            var agentRepository = _unitOfWork.GetRepository<Agent>();
            var currentAgent = agentRepository.Get(x => x.UserName == model.CurrentAgent).SingleOrDefault();

            existingPatient.Id = model.Id;
            existingPatient.Contact = contact;
            existingPatient.Profile = profile;
            existingPatient.UserName = model.UserName;
            existingPatient.Agent = currentAgent;
            existingPatient.AgentId = currentAgent.Id;

            return existingPatient;
        }
    }
}

using System.Linq;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.AgentModels.Impl;
using MyVanity.Model.ConsentFormModels.Impl;
using MyVanity.Model.DoctorModels.Impl;
using MyVanity.Model.FileModels.Impl;
using MyVanity.Model.PlaceModels;
using MyVanity.Model.ProcedureModels.Impl;

namespace MyVanity.Model.PatientProcedureModels.Impl
{
    public class PatientProcedureModelConverter : IPatientProcedureModelConverter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModelConverter<Procedure, ProcedureEditModel> _procedureModelConverter;
        private readonly IModelConverter<Doctor, DoctorEditModel> _doctorModelConverter;
        private readonly IModelConverter<Agent, AgentEditModel> _agentModelConverter;
        private readonly IModelConverter<SharedDocument, FileEditModel> _sharedFileModelConverter;
        private readonly IModelConverter<UserProcedurePatientDocument, FileEditModel> _docFileModelConverter;
        private readonly IModelConverter<Place, PlaceEditModel> _placeModelConverter;
        private readonly IModelConverter<UserProcedureConsentSign, ProcedureConsentViewModel> _procedureConsentConverter;
        private readonly IModelConverter<ConsentForm, ConsentFormEditModel> _consentFormModelConverter; 

        public PatientProcedureModelConverter(IUnitOfWork unitOfWork, IModelConverter<Procedure, ProcedureEditModel> procedureModelConverter, 
                                              IModelConverter<Doctor, DoctorEditModel> doctorModelConverter, 
                                              IModelConverter<Agent, AgentEditModel> agentModelConverter, 
                                              IModelConverter<SharedDocument, FileEditModel> sharedFileModelConverter, 
                                              IModelConverter<UserProcedurePatientDocument, FileEditModel> docFileModelConverter, 
                                              IModelConverter<Place, PlaceEditModel> placeModelConverter, 
                                              IModelConverter<UserProcedureConsentSign, ProcedureConsentViewModel> procedureConsentConverter, 
                                              IModelConverter<ConsentForm, ConsentFormEditModel> consentFormModelConverter)
        {
            _unitOfWork = unitOfWork;
            _procedureModelConverter = procedureModelConverter;
            _doctorModelConverter = doctorModelConverter;
            _agentModelConverter = agentModelConverter;
            _sharedFileModelConverter = sharedFileModelConverter;
            _docFileModelConverter = docFileModelConverter;
            _placeModelConverter = placeModelConverter;
            _procedureConsentConverter = procedureConsentConverter;
            _consentFormModelConverter = consentFormModelConverter;
        }

        public PatientProcedureEditModel ConvertToModel(UserProcedure entity)
        {
            var model = BuildModel(new PatientProcedureEditModel());
            
            model.Id = entity.Id;
            model.PatientId = entity.PatientId;
            model.PatientName = entity.Patient.Profile.FirstName;
            model.SelectedDoctors = entity.Doctors.Select(x => x.Id).ToList();
            model.SelectedProcedure = entity.ProcedureId;
            model.SelectedFinancialCoordinator = entity.Agents.Single(x => x.Type == AgentType.FinanceCoordinator).Id;
            model.SelectedConsents = entity.UserProcedureConsentSigns.Select(x => _procedureConsentConverter.ConvertToModel(x)).ToList();

            var homeAway = entity.Agents.SingleOrDefault(x => x.Type == AgentType.HomeAwayAssistant);
            
            if (homeAway != null)
                model.SelectedHomeAwayAssistant = homeAway.Id;

            model.SelectedMedicalAssistant = entity.Agents.Single(x => x.Type == AgentType.MedicalAssistant).Id;
            model.SurgicalCoordinator = _agentModelConverter.ConvertToModel(entity.Patient.Agent);
            model.AddedFiles = entity.SharedDocuments.Select(x => _sharedFileModelConverter.ConvertToModel(x)).ToList();
            model.PatientDocuments = entity.UserProcedurePatientDocuments.Select(x => _docFileModelConverter.ConvertToModel(x)).ToList();
            model.SelectedPlace = entity.Place == null ? 0 : entity.Place.Id;
            
            return model;
        }

        public UserProcedure ConvertToSource(PatientProcedureEditModel model)
        {
            var existing = _unitOfWork.GetRepository<UserProcedure>().FindById(model.Id) ?? new UserProcedure();

            var selectedProcedure = _unitOfWork.GetRepository<Procedure>().FindById(model.SelectedProcedure);
            var patient = _unitOfWork.GetRepository<Patient>().FindById(model.PatientId.Value);

            existing.Procedure = selectedProcedure;
            existing.ProcedureId = model.SelectedProcedure;
            existing.Patient = patient;
            existing.PatientId = patient.Id;
            existing.PlaceId = model.SelectedPlace;
            return existing;
        }

        public PatientProcedureEditModel BuildModel(PatientProcedureEditModel model)
        {
            var proceduresRepository = _unitOfWork.GetRepository<Procedure>();
            var procedures = proceduresRepository.Get();
            model.Procedures = procedures.Select(x => _procedureModelConverter.ConvertToModel(x)).ToList();

            var doctorsRepository = _unitOfWork.GetRepository<Doctor>();
            var doctors = doctorsRepository.Get(); 
            model.Doctors = doctors.Select(x => _doctorModelConverter.ConvertToModel(x)).ToList();

            var consentRepository = _unitOfWork.GetRepository<ConsentForm>();
            model.Consents = consentRepository.Get().Select(x => _consentFormModelConverter.ConvertToModel(x)).ToList();

            var agentRepository = _unitOfWork.GetRepository<Agent>();
            
            var financials = agentRepository.Get(x => x.Type == AgentType.FinanceCoordinator);
            model.FinancialCoordinators = financials.Select(x => _agentModelConverter.ConvertToModel(x)).ToList();
            
            var homeAways = agentRepository.Get(x => x.Type == AgentType.HomeAwayAssistant);
            model.HomeAwayAssistants = homeAways.Select(x => _agentModelConverter.ConvertToModel(x)).ToList();

            var medical = agentRepository.Get(x => x.Type == AgentType.MedicalAssistant);
            model.MedicalAssistants = medical.Select(x => _agentModelConverter.ConvertToModel(x)).ToList();

            var placeRepository = _unitOfWork.GetRepository<Place>();
            model.Places = placeRepository.Get().Select(x => _placeModelConverter.ConvertToModel(x)).ToList();

            return model;
        }
    }
}

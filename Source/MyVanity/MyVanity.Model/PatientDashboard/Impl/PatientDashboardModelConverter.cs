using System.Linq;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.AgentModels.Impl;
using MyVanity.Model.AppointmentModels.Impl;
using MyVanity.Model.ConsentFormModels.Impl;
using MyVanity.Model.MessageModels;
using MyVanity.Model.PatientModels.Impl;
using MyVanity.Model.PatientProcedureModels.Impl;

namespace MyVanity.Model.PatientDashboard.Impl
{
    public class PatientDashboardModelConverter : IPatientDashboardModelConverter
    {
        private readonly IModelConverter<Patient, PatientEditModel> _patientModelConverter;
        private readonly IModelConverter<UserProcedure, PatientProcedureEditModel> _procedureModelConverter;
        private readonly IModelConverter<Agent, AgentEditModel> _agentModelConverter;
        private readonly IModelConverter<Appointment, AppointmentEditModel> _appointmentModelConverter;
        private readonly IModelConverter<Message, MessageEditModel> _messageModelConverter;
        private readonly IModelConverter<UserProcedureConsentSign, ProcedureConsentViewModel> _consentModelConverter; 
        private readonly IUnitOfWork _unitOfWork;

        public PatientDashboardModelConverter(IModelConverter<Patient, PatientEditModel> patientModelConverter, 
                                              IModelConverter<UserProcedure, PatientProcedureEditModel> procedureModelConverter, 
                                              IModelConverter<Agent, AgentEditModel> agentModelConverter, IUnitOfWork unitOfWork, 
                                              IModelConverter<Appointment, AppointmentEditModel> appointmentModelConverter, 
                                              IModelConverter<Message, MessageEditModel> messageModelConverter, 
                                              IModelConverter<UserProcedureConsentSign, ProcedureConsentViewModel> consentModelConverter)
        {
            _patientModelConverter = patientModelConverter;
            _procedureModelConverter = procedureModelConverter;
            _agentModelConverter = agentModelConverter;
            _unitOfWork = unitOfWork;
            _appointmentModelConverter = appointmentModelConverter;
            _messageModelConverter = messageModelConverter;
            _consentModelConverter = consentModelConverter;
        }

        public IModelConverter<Message, MessageEditModel> MessageModelConverter
        {
            get { return _messageModelConverter; }
        }

        public PatientDashboardViewModel BuildModel(int patientId, int procedureId)
        {
            var patient = _unitOfWork.GetRepository<Patient>().FindById(patientId);
            var patientModel = _patientModelConverter.ConvertToModel(patient);

            var agent = _unitOfWork.GetRepository<Agent>().FindById(patient.AgentId);
            var agentModel = _agentModelConverter.ConvertToModel(agent);

            var dashboardModel = new PatientDashboardViewModel { Patient = patientModel, Agent = agentModel };
            var procedures = patient.Procedures;

            dashboardModel.InboxMessages = patient.Inbox.Select(x => _messageModelConverter.ConvertToModel(x)).ToList();
            dashboardModel.SentMessages = patient.Outbox.Select(x => _messageModelConverter.ConvertToModel(x)).ToList();

            if (procedures == null || procedures.Count == 0) return dashboardModel;
            
            procedureId = procedureId == 0 ? procedures.First().Id : procedureId;

            var procedure = _unitOfWork.GetRepository<UserProcedure>().FindById(procedureId);
            var procedureModel = _procedureModelConverter.ConvertToModel(procedure);
            dashboardModel.PatientProcedure = procedureModel;

            var consents = procedure.UserProcedureConsentSigns;

            if (consents != null && consents.Count != 0)
                dashboardModel.Consents = consents.Select(x => _consentModelConverter.ConvertToModel(x)).ToList();

            var signedConsents = procedure.UserProcedureConsentSigns;
            if (signedConsents != null)
                dashboardModel.SignedConsents = signedConsents.Where(x => x.Signed).Select(x => x.ConsentFormId).ToList();

            var appointments = procedure.Appointments;
            if (appointments != null && appointments.Count != 0)
            {
                var appointmentsModel = appointments.Select(x => _appointmentModelConverter.ConvertToModel(x)).ToList();
                dashboardModel.Appointments = appointmentsModel;
            }

            if (procedures.Count <= 1) return dashboardModel;
            
            var existingProcedures = procedures.ToDictionary(item => item.Id, item => string.Format("{0} {1}", item.Procedure.Category.Name, item.Procedure.Type.Name));
            dashboardModel.ScheduledProcedures = existingProcedures;

            return dashboardModel;
        }
    }
}

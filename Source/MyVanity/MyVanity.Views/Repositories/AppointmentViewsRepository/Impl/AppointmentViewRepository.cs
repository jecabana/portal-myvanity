using System.Collections.Generic;
using System.Linq;
using MyVanity.Common;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.AppointmentModels.Impl;
using MyVanity.Services.MailServices;

namespace MyVanity.Views.Repositories.AppointmentViewsRepository.Impl
{
    public class AppointmentViewRepository : ViewRepository<Appointment, AppointmentEditModel>,IAppointmentViewRepository
    {
        private readonly IModelConverter<Appointment, AppointmentEditModel> _modelConverter;
        private readonly IMessageCenter _messageCenter;

        public AppointmentViewRepository(IModelConverter<Appointment, AppointmentEditModel> modelConverter, IUnitOfWork unitOfWork, IMessageCenter messageCenter) : base(modelConverter, unitOfWork)
        {
            _modelConverter = modelConverter;
            _messageCenter = messageCenter;
        }

        public IEnumerable<AppointmentEditModel> GetAppointmentsForAgent(int agentId)
        {
            var agent = UnitOfWork.GetRepository<Agent>().FindById(agentId);

            var results = new List<AppointmentEditModel>();
            if (agent.Patients == null)
                return results;

            foreach (var procedure in agent.Patients.SelectMany(patient => patient.Procedures))
            {
                results.AddRange(procedure.Appointments.Select(x => _modelConverter.ConvertToModel(x)));
            }

            return results;
        }

        public AppointmentStatus ChangeStatus(int id, AppointmentStatus newStatus)
        {
            var repository = UnitOfWork.GetRepository<Appointment>();
            var appointment = repository.FindById(id);
            appointment.Status = newStatus;
            
            UnitOfWork.SaveChanges();
            return appointment.Status;
        }

        public override void Insert(AppointmentEditModel model)
        {
            var entity = _modelConverter.ConvertToSource(model);
            entity.Status = AppointmentStatus.Scheduled;
            UnitOfWork.GetRepository<Appointment>().Insert(entity);
            UnitOfWork.SaveChanges();

            var procedureRepository = UnitOfWork.GetRepository<UserProcedure>();
            var procedure = procedureRepository.FindById(entity.UserProcedureId);
            var patient = procedure.Patient;

            _messageCenter.SendEmailMessage("AppointmentCreated", new
                                                                  {
                                                                      Name = patient.Profile.FirstName + " " + patient.Profile.LastName,
                                                                      Date = model.Date.Value.ToString(Conventions.ShortDateFormat),
                                                                      ProcedureDetails = string.Format("{0} {1}", procedure.Procedure.Category.Name, procedure.Procedure.Type.Name), model.Description
                                                                  },  patient.Email, Constants.VanityMail, "Appointment Scheduled", null);
        }
    }
}

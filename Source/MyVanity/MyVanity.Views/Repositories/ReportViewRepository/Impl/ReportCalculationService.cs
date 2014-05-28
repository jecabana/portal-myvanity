using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.AppointmentModels.Impl;
using MyVanity.Model.ConsentFormModels.Impl;
using MyVanity.Model.MessageModels;
using MyVanity.Model.UserModels;

namespace MyVanity.Views.Repositories.ReportViewRepository.Impl
{
    public class ReportCalculationService : IReportCalculationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportCalculationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ConsentReportViewModel> CalculateUnsignedConsents(int daysToProcedure = 5)
        {
            var consentRepository = _unitOfWork.GetRepository<UserProcedureConsentSign>();
            var consents = consentRepository.Query();

            var result = from consent in consents
                         where !consent.Signed 
                         select new ConsentReportViewModel
                         {
                             PatientName = consent.UserProcedure.Patient.Profile.FirstName + " " + consent.UserProcedure.Patient.Profile.MiddleName,
                             ProcedureName = consent.UserProcedure.Procedure.Category.Name + " " + consent.UserProcedure.Procedure.Type.Name,
                             ConsentTitle = consent.ConsentForm.Title
                         };

            return result.ToList();
        }

        public List<AppointmentReportViewModel> CalculateUnconfirmedAppointments(int daysToAppointment = 5)
        {
            var appointmentsRepository = _unitOfWork.GetRepository<Appointment>();
            var appointments = appointmentsRepository.Query();

            var procedureRepository = _unitOfWork.GetRepository<UserProcedure>();
            var procedures = procedureRepository.Query();

            var result = from appointment in appointments
                         join procedure in procedures
                         on appointment.UserProcedureId equals procedure.Id
                         let difference = appointment.Date.Day - DateTime.Now.Day
                         where (appointment.Status == AppointmentStatus.Scheduled 
                                && difference >= 0 && difference <= daysToAppointment)
                        select new AppointmentReportViewModel
                        {
                            Patient = procedure.Patient.Profile.FirstName + " " + procedure.Patient.Profile.MiddleName,
                            AppointmentDate = appointment.Date,
                            AppointmentDescription = appointment.Description,
                            PatientNumber = procedure.Patient.Id
                        };

            return result.ToList();
        }

        public List<MessageReportViewModel> CalculateUnansweredEmails(int afterDaysSent)
        {
            var messageRepository = _unitOfWork.GetRepository<Message>();
            var messages = messageRepository.Query();

            var unanswered = from item in messages
                             where !(from replied in messages
                                     where replied.RepliesTo != null 
                                     select replied.RepliesTo.Id).Contains(item.Id) && DateTime.Now.Day - item.Date.Day >= afterDaysSent
                             select new MessageReportViewModel
                                    {
                                        Body = item.Body,
                                        Id = item.Id,
                                        From = item.From.UserName,
                                        IsRead = item.IsRead,
                                        To = item.To.UserName,
                                        Date = item.Date
                                    };

            return unanswered.ToList();
        }


    }
}

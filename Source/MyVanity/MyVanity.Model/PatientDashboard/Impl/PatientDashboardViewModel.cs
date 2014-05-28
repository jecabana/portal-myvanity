using System.Collections.Generic;
using System.Linq;
using MyVanity.Domain;
using MyVanity.Model.AgentModels.Impl;
using MyVanity.Model.AppointmentModels.Impl;
using MyVanity.Model.ConsentFormModels.Impl;
using MyVanity.Model.DoctorModels.Impl;
using MyVanity.Model.FileModels.Impl;
using MyVanity.Model.MessageModels;
using MyVanity.Model.PatientModels.Impl;
using MyVanity.Model.PatientProcedureModels.Impl;

namespace MyVanity.Model.PatientDashboard.Impl
{
    public class PatientDashboardViewModel
    {
        //Current Patient
        public PatientEditModel Patient { get; set; }

        //Patient's Agent Coordinator
        public AgentEditModel Agent { get; set; }

        //Current Patient Procedure
        public PatientProcedureEditModel PatientProcedure { get; set; }

        //Consent Forms
        public List<ProcedureConsentViewModel> Consents { get; set; }

        public bool HasAnyConsent
        {
            get { return  Consents != null && Consents.Count() != 0; }
        }

        //{id, name} Scheduled Procedures
        public Dictionary<int, string> ScheduledProcedures { get; set; }

        //Appointments for current procedure
        public List<AppointmentEditModel> Appointments { get; set; }

        public int ScheduledAppointments
        {
            get { return Appointments == null ? 0 : Appointments.Count(x => x.StatusEnum == AppointmentStatus.Scheduled); }

        }

        public bool HasAnyAppointment
        {
            get { return Appointments != null && Appointments.Count != 0; }
             
        }

        //Patient's Inbox messages
        public List<MessageEditModel> InboxMessages { get; set; }

        //Patient's Outbox messages
        public List<MessageEditModel> SentMessages { get; set; }

        //Lead procedure doctor
        public DoctorEditModel Doctor
        {
            get { return HasProcedure ? PatientProcedure.AddedDoctors.First() : null; }
        }

        //Patient's Procedure own documents
        public List<FileEditModel> PatientDocuments
        {
            get { return PatientProcedure == null ? new List<FileEditModel>() 
                                                  : (PatientProcedure.PatientDocuments ?? new List<FileEditModel>()); }
        }

        //Procedure Documents managed by Agents
        public List<FileEditModel> SharedDocuments
        {
            get
            {
                return PatientProcedure == null ? new List<FileEditModel>()
                                                : (PatientProcedure.AddedFiles ?? new List<FileEditModel>());
            }
        }

        //Procedure Videos managed by Agents
        public List<FileEditModel> Videos
        {
            get { return SharedDocuments.Where(x => x.ContentType == ContentType.Video).ToList(); }
        }

        //Procedure Articles managed by Agents
        public List<FileEditModel> Articles
        {
            get { return SharedDocuments.Where(x => x.ContentType == ContentType.Word || x.ContentType == ContentType.Pdf).ToList(); }
        }

        //Indicates wether there is an existing procedure or not
        public bool HasProcedure
        {
            get { return PatientProcedure != null; }

        }

        //Time remaining for reaching current's procedure Date
        /*public int? CalculateMissingDays()
        {
            if (PatientProcedure == null || PatientProcedure.Date == null) return null;
            
            return (PatientProcedure.Date - DateTime.Now).Value.Days;
        }*/

        //Time remaining for reaching current's procedure Date
        /*public int? CalculateMissingHours()
        {
            if (PatientProcedure == null || PatientProcedure.Date == null) return null;

            return (PatientProcedure.Date - DateTime.Now).Value.Hours;
        }*/

        //Determines wether the current procedure is today or not
        /*public bool? IsProcedureToday 
        {
            get
            {
                if (PatientProcedure != null && PatientProcedure.Date != null)
                {
                    return Helper.EqualDates(PatientProcedure.Date.Value, DateTime.Now);
                }

                return null;
            }
        }*/

        //List containing Ids of signed user procedures
        public List<int> SignedConsents { get; set; }

        //Helper Methods

        //Amount of unread messages
        public int UnreadMessages
        {
            get { return InboxMessages == null ? 0 : InboxMessages.Count(x => !x.IsRead); }
        }

        //Amount of unsigned consents for the current procedure
        public int UnsignedConsentsAmount 
        {
            get { return Consents == null ? 0 : Consents.Count - SignedConsents.Count; }
        }

        public AgentEditModel FinancialCoordinator 
        {
            get { return PatientProcedure == null ? null : PatientProcedure.FinancialCoordinator; }
        }

        public AgentEditModel HomeAwayAssistant
        {
            get { return PatientProcedure == null ? null : PatientProcedure.HomeAwayAssistant; }
        }

        public AgentEditModel MedicalAssistant
        {
            get { return PatientProcedure == null ? null : PatientProcedure.MedicalAssistant; }
        }

        public bool IsConsentSigned(int id)
        {
            return SignedConsents != null && SignedConsents.Contains(id);
        }

        public bool IsMale 
        {
            get { return Patient.Profile.IsMale; }
        }

        public string Genre
        {
            get { return IsMale ? "Male" : "Female"; }
        }
    }
}

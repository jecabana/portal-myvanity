using System;
using System.Collections.Generic;
using System.Linq;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Services.MailServices;

namespace MyVanity.Services.UserProcedureConsentServices.Impl
{
    public class UserProcedureConsentService : IUserProcedureConsentService
    {
        private readonly IMessageCenter _mailService;
        private readonly IUnitOfWork _unitOfWork;

        public UserProcedureConsentService(IUnitOfWork unitOfWork, IMessageCenter mailService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }

        public void SignConsents(int procedureId, List<int> consents)
        {
            //Get all repositories
            var repository = _unitOfWork.GetRepository<UserProcedureConsentSign>();
            var userProcedureRepository = _unitOfWork.GetRepository<UserProcedure>();

            //Get current User Procedure
            var userProcedure = userProcedureRepository.FindById(procedureId);
            //Get Patient
            var patient = userProcedure.Patient;
            //Get receipt emails
            var emailReceipts = string.Join(",", userProcedure.Agents.Select(x => x.Email));

            foreach (var id in consents)
            {
                //Get current consent
                var consent = repository.FindById(id);
                //Sign it
                consent.Signed = true;
                //razor view model
                var viewModel = new
                                {
                                    ConsentTitle = consent.ConsentForm.Title,
                                    ProcedureDetails = userProcedure.ShortDescription,
                                    SignedDate = DateTime.Now,
                                    PatientName = patient.Profile.FullName
                                };

                _mailService.SendEmailMessage("Notification_To_Agent_When_ConsentForm_Signed", viewModel, emailReceipts, patient.Email, "Patient Consent Signed");
            }

            _unitOfWork.SaveChanges();
        }
    }
}

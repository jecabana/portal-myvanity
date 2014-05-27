using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyVanity.Common;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.PatientDashboard;
using MyVanity.Model.PatientModels.Impl;
using MyVanity.Services.Blobs;
using MyVanity.Services.Membership;
using MyVanity.Services.UserProcedureConsentServices;
using MyVanity.Views.Repositories.AppointmentViewsRepository;
using MyVanity.Views.Repositories.PatientViewsRepository;
using MyVanity.Web.Controllers.Base;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientDashboardController : FileManagingController
    {
        private readonly IPatientViewRepository _patientViewRepository;
        private readonly IPatientDashboardModelConverter _dashboardModelConverter;
        private readonly IAppointmentViewRepository _appointmentViewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProcedureConsentService _userProcedureConsentService;

        public PatientDashboardController(IMembershipService membershipService, 
                                          IBlobStore blobStore, IPatientDashboardModelConverter dashboardModelConverter, 
                                          IPatientViewRepository patientViewRepository, IUnitOfWork unitOfWork, 
                                          IAppointmentViewRepository appointmentViewRepository, 
                                          IUserProcedureConsentService userProcedureConsentService) : base(membershipService, blobStore)
        {
            _dashboardModelConverter = dashboardModelConverter;
            _patientViewRepository = patientViewRepository;
            _unitOfWork = unitOfWork;
            _appointmentViewRepository = appointmentViewRepository;
            _userProcedureConsentService = userProcedureConsentService;
        }

        private PatientEditModel Patient
        {
            get { return _patientViewRepository.FindById(CurrentUser.OwnerId); }
        }

        public ActionResult Dashboard(int? procedureId)
        {
            
            var procId = procedureId ?? 0;
            var model = _dashboardModelConverter.BuildModel(Patient.Id, procId);
            return View(model);
        }

        public ActionResult PatientProfile()
        {
            return View(Patient);
        }

        [HttpPost]
        public ActionResult PatientProfile(PatientEditModel model)
        {
            if (ModelState.IsValid)
            {
                _patientViewRepository.Update(model);
                return RedirectToAction("Dashboard");
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult UploadDocument(HttpPostedFileBase file, string description, int userProcedureId)
        {
            var patientId = CurrentUser.OwnerId;
            var fileResult = Upload(file,Constants.DocsContainer, Constants.PatientProcedureDocs);

            if (fileResult.Success)
            {
                try
                {
                    var catRepository = _unitOfWork.GetRepository<DocumentCategory>();
                    var subCatRepository = _unitOfWork.GetRepository<DocumentSubcategory>();

                    var patientDocument = new UserProcedurePatientDocument
                    {
                        Category = catRepository.Get(x => x.Name == "Non Medical").Single(),
                        Subcategory = subCatRepository.Get(x => x.Name == "Patient Information").Single(),
                        ContentType = Domain.Helpers.ResolveContentTypeFromName(file.FileName),
                        Description = description,
                        Name = file.FileName,
                        PatientId = patientId,
                        Path = fileResult.Path,
                        UserProcedureId = userProcedureId,
                        RealName = file.FileName
                    };

                    var documentRepository = _unitOfWork.GetRepository<UserProcedurePatientDocument>();
                    documentRepository.Insert(patientDocument);
                    _unitOfWork.SaveChanges();
                }
                catch (Exception)
                {
                    DeleteFile(Constants.DocsContainer, fileResult.Path);
                    return FileJsonResult(file.FileName, file.ContentLength, false, fileResult.Path);
                }
            }

            return FileJsonResult(file.FileName, file.ContentLength, fileResult.Success, fileResult.Path);
        }

        [HttpPost]
        public JsonResult ChangeAppointmentStatus(AppointmentStatus newStatus, int appointmentId)
        {
            var result = _appointmentViewRepository.ChangeStatus(appointmentId, newStatus);
            return Json(new
                        {
                            status = Enum.GetName(typeof (AppointmentStatus), result)
                        });
        }

        [HttpPost]
        public JsonResult SignConsent(int procedureId, List<int> consentIds)
        {
            try
            {
                _userProcedureConsentService.SignConsents(procedureId, consentIds);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
    }
}
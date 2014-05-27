using System.Threading.Tasks;
using System.Web.Mvc;
using MyVanity.Model.DoctorModels.Impl;
using MyVanity.Services.Blobs;
using MyVanity.Services.Membership;
using MyVanity.Views.Repositories;
using MyVanity.Views.Repositories.ReportViewRepository;
using MyVanity.Web.Controllers.Base;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : FileManagingController
    {
        private readonly IViewRepository<DoctorEditModel> _doctorViewRepository;
        private readonly IReportCalculationService _reportService;

        public AdminController(IMembershipService membershipService, IViewRepository<DoctorEditModel> doctorViewRepository, IBlobStore blobStore, IReportCalculationService reportService) : base(membershipService, blobStore)
        {
            _doctorViewRepository = doctorViewRepository;
            _reportService = reportService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Agent");
        }

        public ActionResult DoctorsList()
        {
            var doctorsRepository = _doctorViewRepository.GetAll();
            return View(doctorsRepository);
        }

        public ActionResult CreateDoctor()
        {
            return View(new DoctorEditModel());
        }

        public async Task<ActionResult> EditDoctor(int id)
        {
            var doctor = await _doctorViewRepository.FindAsync(id);            
            return View(doctor);
        }

        [HttpPost]
        public ActionResult EditDoctor(DoctorEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                _doctorViewRepository.Update(editModel);
                return RedirectToAction("DoctorsList");
            }

            return View(editModel);
        }

        [HttpPost]
        public ActionResult CreateDoctor(DoctorEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                _doctorViewRepository.Insert(editModel);
                return RedirectToAction("DoctorsList");
            }

            return View(editModel);
        }

        public ActionResult DeleteDoctor(int id)
        {
            _doctorViewRepository.Delete(id);
            return RedirectToAction("DoctorsList");
        }

        public ActionResult ConsentReports()
        {
            var model = _reportService.CalculateUnsignedConsents();
            return View(model);
        }

        public ActionResult AppointmentReports()
        {
            var model = _reportService.CalculateUnconfirmedAppointments();
            return View(model);
        }

        public ActionResult UnansweredEmails(int afterDaysSent = 2)
        {
            var model = _reportService.CalculateUnansweredEmails(afterDaysSent);
            return View(model);
        }
    }
}
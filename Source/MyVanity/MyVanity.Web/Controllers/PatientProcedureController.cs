using System.Threading.Tasks;
using System.Web.Mvc;
using MyVanity.Model.PatientProcedureModels;
using MyVanity.Model.PatientProcedureModels.Impl;
using MyVanity.Services.Membership;
using MyVanity.Views.Repositories.PatientProcedure;
using MyVanity.Web.Controllers.Base;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles = "Agent, Admin")]
    public class PatientProcedureController : BaseController
    {
        private readonly IPatientProcedureModelConverter _modelConverter;
        private readonly IPatientProcedureViewRepository _patientProcedureViewRepository;
        private readonly IMembershipService _membershipService;

        public PatientProcedureController(IPatientProcedureModelConverter modelConverter, IPatientProcedureViewRepository patientProcedureViewRepository, 
                                          IMembershipService membershipService) : base(membershipService)
        {
            _modelConverter = modelConverter;
            _patientProcedureViewRepository = patientProcedureViewRepository;
            _membershipService = membershipService;
        }

        public async Task<ActionResult> Index()
        {
            var isAdmin = _membershipService.IsInRole(CurrentUser, ApplicationRole.Admin);
            var existingProcedures = await isAdmin ? _patientProcedureViewRepository.GetAll() 
                                                   : _patientProcedureViewRepository.GetUserProcedures(CurrentUser.OwnerId);

            return View(new PatientProcedureIndexModel(existingProcedures));
        }

        [Authorize(Roles = "SurgicalCoordinator")]
        public ActionResult Create()
        {
            var model = _modelConverter.BuildModel(new PatientProcedureEditModel());
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SurgicalCoordinator")]
        public ActionResult Create(PatientProcedureEditModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _patientProcedureViewRepository.Insert(viewModel);
                return RedirectToAction("Index");
            }

            var rebuilt = _modelConverter.BuildModel(viewModel);
            return View(rebuilt);
        }

        [Authorize(Roles = "SurgicalCoordinator, Admin")]
        public ActionResult Delete(int id)
        {
            _patientProcedureViewRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var procedure = _patientProcedureViewRepository.FindById(id);
            return View(procedure);
        }

        [HttpPost]
        public ActionResult Edit(PatientProcedureEditModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _patientProcedureViewRepository.Update(viewModel);
                return RedirectToAction("Index");
            }

            var rebuilt = _modelConverter.BuildModel(viewModel);
            return View(rebuilt);
        }

        public PartialViewResult GetProcedures(string name)
        {
            var model = _patientProcedureViewRepository.GetUserProcedures(CurrentUser.OwnerId, name);
            return PartialView("_ProcedureListPartial", model);
        }
    }
}
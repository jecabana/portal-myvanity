using System.Threading.Tasks;
using System.Web.Mvc;
using MyVanity.Model.ProcedureTypeModels.Impl;
using MyVanity.Views.Repositories;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProcedureTypeController : Controller
    {
        private readonly IViewRepository<ProcedureTypeEditModel> _typesRepository;

        public ProcedureTypeController(IViewRepository<ProcedureTypeEditModel> typesRepository)
        {
            _typesRepository = typesRepository;
        }

        public ActionResult Index()
        {
            var categories = _typesRepository.GetAll();
            
            return View(new ProcedureTypeIndexModel(categories));
        }

        public ActionResult Create()
        {
            return View(new ProcedureTypeEditModel());
        }

        [HttpPost]
        public ActionResult Create(ProcedureTypeEditModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _typesRepository.Insert(viewModel);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var typeViewModel = await _typesRepository.FindAsync(id);
            return View(typeViewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProcedureTypeEditModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _typesRepository.Update(viewModel);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public ActionResult Delete(int id)
        {
            _typesRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
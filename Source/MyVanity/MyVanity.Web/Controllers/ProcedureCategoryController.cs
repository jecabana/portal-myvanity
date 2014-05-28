using System.Threading.Tasks;
using System.Web.Mvc;
using MyVanity.Model.ProcedureCategoryModels.Impl;
using MyVanity.Views.Repositories;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProcedureCategoryController : Controller
    {
        private readonly IViewRepository<ProcedureCategoryViewModel> _categoryRepository; 

        public ProcedureCategoryController(IViewRepository<ProcedureCategoryViewModel> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            var categories = _categoryRepository.GetAll();
            return View(new ProcedureCategoryIndexModel(categories));
        }

        public ActionResult Create()
        {
            return View(new ProcedureCategoryViewModel());
        }

        [HttpPost]
        public ActionResult Create(ProcedureCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Insert(viewModel);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var categoryViewModel = await _categoryRepository.FindAsync(id);
            return View(categoryViewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProcedureCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(viewModel);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public ActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
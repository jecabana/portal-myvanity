using System.Web.Mvc;
using MyVanity.Model.ProcedureModels;
using MyVanity.Model.ProcedureModels.Impl;
using MyVanity.Views.Repositories;

namespace MyVanity.Web.Controllers
{
    public class ProcedureController : Controller
    {
        private readonly IProcedureModelConverter _procedureModelConverter;
        private readonly IViewRepository<ProcedureEditModel> _procedureRepository;

        public ProcedureController(IProcedureModelConverter procedureModelConverter, IViewRepository<ProcedureEditModel> procedureRepository)
        {
            _procedureModelConverter = procedureModelConverter;
            _procedureRepository = procedureRepository;
        }

        public ActionResult Index()
        {
            var procedures = _procedureRepository.GetAll();
            return View(new ProcedureIndexModel(procedures));
        }

        public ActionResult Create()
        {
            var viewModel = _procedureModelConverter.BuildModel(new ProcedureEditModel());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(ProcedureEditModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _procedureRepository.Insert(viewModel);
                return RedirectToAction("Index");
            }

            var rebuilt = _procedureModelConverter.BuildModel(viewModel);
            return View(rebuilt);
        }

        public ActionResult Edit(int id)
        {
            var viewModel = _procedureRepository.FindById(id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProcedureEditModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _procedureRepository.Update(viewModel);
                return RedirectToAction("Index");
            }

            var rebuilt = _procedureModelConverter.BuildModel(viewModel);
            return View(rebuilt);
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyVanity.Common;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.DocumentCategoryModels.Impl;
using MyVanity.Model.FileModels.Impl;
using MyVanity.Model.ResourceModels;
using MyVanity.Services.Blobs;
using MyVanity.Services.Files;
using MyVanity.Services.Membership;
using MyVanity.Views.Repositories;
using MyVanity.Web.Controllers.Base;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles = "Agent, Admin")]
    public class ResourcesController : FileManagingController
    {
        private readonly IViewRepository<FileEditModelSync> _viewRepository;
        private readonly IViewModelBuilder<FileEditModelSync> _modelBuilder;
        private readonly IEntityConverter<DocumentSubcategory, CategoryEditModel> _catConverter;
        private readonly IModelConverter<SharedDocument, FileEditModelSync> _fileModelConverter;
        private readonly IViewRepository<UserProcedurePatientDocViewModel> _patientDocViewRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;


        public ResourcesController(IMembershipService membershipService,
                                   IBlobStore blobStore, IViewRepository<FileEditModelSync> viewRepository,
                                   IViewModelBuilder<FileEditModelSync> modelBuilder, IEntityConverter<DocumentSubcategory, CategoryEditModel> catConverter, 
                                   IModelConverter<SharedDocument, FileEditModelSync> fileModelConverter, IViewRepository<UserProcedurePatientDocViewModel> patientDocViewRepository, 
                                   IFileService fileService, IUnitOfWork unitOfWork) : base(membershipService, blobStore)
        {
            _viewRepository = viewRepository;
            _modelBuilder = modelBuilder;
            _catConverter = catConverter;
            _fileModelConverter = fileModelConverter;
            _patientDocViewRepository = patientDocViewRepository;
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var resources = _viewRepository.GetAll();
            return View(resources);
        }

        public ActionResult Edit(int id)
        {
            var model = _viewRepository.FindById(id);
            return View(_modelBuilder.BuildModel(model));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(FileEditModelSync model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    var result = await UploadAsync(model.File, Constants.DocsContainer, Constants.SharedResources);
                    model.Path = result.Path;
                }

                _viewRepository.Update(model);
                return RedirectToAction("Index");
            }

            var rebuilt = _modelBuilder.BuildModel(model);
            return View(rebuilt);
        }

        public ActionResult Create()
        {
            var model = _modelBuilder.BuildModel(new FileEditModelSync());
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(FileEditModelSync model)
        {
            if (model.File == null)
                ModelState.AddModelError("", "There was an error uploading your resource, please try again");
            
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    var result = await UploadAsync(model.File, Constants.DocsContainer, Constants.SharedResources);

                    if (!result.Success)
                    {
                        ModelState.AddModelError("", "There was an error trying to save the resource, please try again in a few seconds");
                        return View(model);
                    }
                    model.Path = result.Path;
                }

                _viewRepository.Insert(model);
                return RedirectToAction("Index");
            }

            var rebuilt = _modelBuilder.BuildModel(model);
            return View(rebuilt);
        }

        public ActionResult Delete(int id)
        {
            _viewRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public PartialViewResult GetFilesBy(string criteria)
        {
            var repository = _unitOfWork.GetRepository<SharedDocument>();
            var files = repository.Get(x => x.Censured == false
                                            && (x.Name.Contains(criteria) || x.Category.Name.Contains(criteria) || x.Subcategory.Name.Contains(criteria)));

            var model = files.Select(x => _fileModelConverter.ConvertToModel(x));
            return PartialView("_FileListPartial", model);
        }

        public JsonResult GetSubcatsForCat(int catId)
        {
            var repository = _unitOfWork.GetRepository<DocumentSubcategory>();

            var subcats = repository.Get(x => x.CategoryId == catId);
            var model = subcats.Select(x => _catConverter.ConvertToModel(x));

            return Json(new { subcats = model }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadedByPatients()
        {
            var result = _patientDocViewRepository.GetAll();
            return View(result);
        }

        public ActionResult DeleteDoc(int id)
        {
            _patientDocViewRepository.Delete(id);
            return RedirectToAction("UploadedByPatients");
        }

        public ActionResult ChangeDocCensure(int id, bool censure)
        {
            _fileService.ChangeFileCensure(id, censure);
            return RedirectToAction("UploadedByPatients");
        }
    }
}
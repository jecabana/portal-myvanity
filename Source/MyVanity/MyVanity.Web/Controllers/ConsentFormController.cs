using System;
using System.Web;
using System.Web.Mvc;
using MyVanity.Common;
using MyVanity.Model.ConsentFormModels.Impl;
using MyVanity.Services.Blobs;
using MyVanity.Services.DocumentParser;
using MyVanity.Services.Membership;
using MyVanity.Views.Repositories;
using MyVanity.Web.Controllers.Base;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles = "Agent")]
    public class ConsentFormController : FileManagingController
    {
        private readonly IViewRepository<ConsentFormEditModel> _viewRepository;
        private readonly IDocumentParser _documentParser;

        public ConsentFormController(IMembershipService membershipService,
                                     IBlobStore blobStore, IViewRepository<ConsentFormEditModel> viewRepository, 
                                     IDocumentParser documentParser) : base(membershipService, blobStore)
        {
            _viewRepository = viewRepository;
            _documentParser = documentParser;
        }

        public ActionResult Index()
        {
            var consents = _viewRepository.GetAll();
            var model = new ConsentFormIndexModel(consents);

            return View(model);
        }

        public ActionResult Create()
        {
            return View(new ConsentFormEditModel());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ConsentFormEditModel model)
        {
            if (ModelState.IsValid)
            {
                _viewRepository.Insert(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ParseConsentDoc(HttpPostedFileBase file, string relPath)
        {
            var result = new Base.FileResult();
            try
            {
                result = Upload(file, Constants.DocsContainer, relPath ?? Constants.ConsentFormsLocation);
                file.InputStream.Position = 0;
                var text = _documentParser.ParseDocument(file.InputStream);

                return Json(new
                            {
                                success = result.Success,
                                body = text,
                                filePath = result.Path
                            });
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(result.Path) && result.Success)
                    DeleteFile(Constants.DocsContainer, result.Path);   
                
                return Json(new
                            {
                                message = ex.Message,
                                success = false
                            });
            }
        }

        public ActionResult Delete(int id)
        {
            _viewRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
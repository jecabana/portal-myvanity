using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyVanity.Common;
using MyVanity.Services.Blobs;
using MyVanity.Services.Membership;

namespace MyVanity.Web.Controllers.Base
{
    public class FileManagingController : BaseController
    {
        private readonly IBlobStore _blobStore;
        private const string SuccessResult = "Successfully uploaded";
        private const string ErrorResult = "An error ocurred while uploading";

        public FileManagingController(IMembershipService membershipService, IBlobStore blobStore) : base(membershipService)
        {
            _blobStore = blobStore;
        }

        protected FileResult Upload(HttpPostedFileBase file, string container, string relPath)
        {
            //Generate guid for the file to upload
            var path = string.Format("{0}/{1:10}{2}_{3}", relPath, DateTime.Now.Ticks, Guid.NewGuid(), Path.GetExtension(file.FileName));

            //Instantiate a new FileWrapper
            var fileWrapper = new FileWrapper
                              {
                                  Stream = file.InputStream,
                                  ContentType = file.ContentType,
                                  Name = file.FileName,
                                  Path = path
                              };

            //Upload the file
            var result = _blobStore.Save(container, fileWrapper); 
            
            return new FileResult
                   {
                       Path = path,
                       Success = result
                   };
        }

        protected async Task<FileResult> UploadAsync(HttpPostedFileBase file, string container, string relPath)
        {
            //Generate guid for the file to upload
            var path = string.Format("{0}/{1:10}{2}_{3}", relPath, DateTime.Now.Ticks, Guid.NewGuid(), Path.GetExtension(file.FileName));

            //Instantiate a new FileWrapper
            var fileWrapper = new FileWrapper
            {
                Stream = file.InputStream,
                ContentType = file.ContentType,
                Name = file.FileName,
                Path = path
            };

            //Upload the file
            var result = await _blobStore.SaveAsync(container, fileWrapper);

            return new FileResult
            {
                Path = path,
                Success = result
            };
        }

        public async Task<FileStreamResult> DownloadFile(string path, string container)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            //Go and fetch file from blobStore
            var newFileWrapper = await _blobStore.FindFileAsync(container ?? Constants.DocsContainer, path);

            if (newFileWrapper == null || newFileWrapper.Stream.Length == 0)
                throw new HttpException(404, "File not found");

            return File(newFileWrapper.Stream, newFileWrapper.ContentType, newFileWrapper.Name);
        }

        public Task<FileStreamResult> DownloadImage(string path)
        {
            return DownloadFile(path, Constants.ImagesContainer);
        }

        [HttpPost]
        public JsonResult UploadFile(HttpPostedFileBase file, string relPath)
        {
            var result = Upload(file, Constants.DocsContainer, relPath);
            return FileJsonResult(file.FileName, file.ContentLength, result.Success, result.Path);
        }

        [HttpPost]
        public JsonResult UploadImage(HttpPostedFileBase file, string relPath)
        {
            var result = Upload(file, Constants.ImagesContainer, relPath);
            return FileJsonResult(file.FileName, file.ContentLength, result.Success, result.Path);
        }

        public JsonResult FileJsonResult(string name, int size, bool success, string guid)
        {
            return Json(new
                        {
                            name,
                            size,
                            success,
                            guid,
                            message = success ? SuccessResult : ErrorResult
                        }, JsonRequestBehavior.AllowGet);
        }

        protected bool DeleteFile(string container, string path)
        {
            try
            {
                _blobStore.DeleteAsync(container, path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class FileResult
    {
        public string Path { get; set; }

        public bool Success { get; set; }
    }
}
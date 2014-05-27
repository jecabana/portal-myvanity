using MyVanity.Domain;
using MyVanity.Domain.UoW;

namespace MyVanity.Services.Files.Impl
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void ChangeFileCensure(int fileId, bool censure)
        {
            var fileRepository = _unitOfWork.GetRepository<Document>();
            var file = fileRepository.FindById(fileId);

            file.Censured = censure;
            _unitOfWork.SaveChanges();
        }
    }
}

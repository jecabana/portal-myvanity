using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.FileModels.Impl;

namespace MyVanity.Views.Repositories.SharedDocumentsViewRepository.Impl
{
    public class SharedDocViewRepository : ViewRepository<SharedDocument, FileEditModelSync>
    {
        public SharedDocViewRepository(IModelConverter<SharedDocument, FileEditModelSync> modelConverter, IUnitOfWork unitOfWork) : base(modelConverter, unitOfWork)
        {
        }
    }
}

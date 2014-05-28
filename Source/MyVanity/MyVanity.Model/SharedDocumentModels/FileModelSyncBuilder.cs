using System.Linq;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.DocumentCategoryModels.Impl;
using MyVanity.Model.FileModels.Impl;

namespace MyVanity.Model.SharedDocumentModels
{
    public class FileModelSyncBuilder : IViewModelBuilder<FileEditModelSync>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityConverter<DocumentCategory, CategoryEditModel> _catModelConverter;
                private readonly IEntityConverter<DocumentSubcategory, CategoryEditModel> _subcatModelConverter;

        public FileModelSyncBuilder(IUnitOfWork unitOfWork, IEntityConverter<DocumentCategory, CategoryEditModel> catModelConverter, 
                                IEntityConverter<DocumentSubcategory, CategoryEditModel> subcatModelConverter)
        {
            _unitOfWork = unitOfWork;
            _catModelConverter = catModelConverter;
            _subcatModelConverter = subcatModelConverter;
        }

        public FileEditModelSync BuildModel(FileEditModelSync model)
        {
            var catRepository = _unitOfWork.GetRepository<DocumentCategory>();
            var subcatRepository = _unitOfWork.GetRepository<DocumentSubcategory>();

            var cats = catRepository.Get();
            var subcats = subcatRepository.Get();

            model.Categories = cats.Select(x => _catModelConverter.ConvertToModel(x)).ToList();
            model.Subcategories = subcats.Select(x => _subcatModelConverter.ConvertToModel(x)).ToList();

            return model;
        }
    }
}

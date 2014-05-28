using System.Linq;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.DocumentCategoryModels.Impl;
using MyVanity.Model.FileModels.Impl;

namespace MyVanity.Model.SharedDocumentModels
{
    public class FileModelBuilder : IViewModelBuilder<FileEditModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityConverter<DocumentCategory, CategoryEditModel> _catModelConverter;
                private readonly IEntityConverter<DocumentSubcategory, CategoryEditModel> _subcatModelConverter;

        public FileModelBuilder(IUnitOfWork unitOfWork, IEntityConverter<DocumentCategory, CategoryEditModel> catModelConverter, 
                                IEntityConverter<DocumentSubcategory, CategoryEditModel> subcatModelConverter)
        {
            _unitOfWork = unitOfWork;
            _catModelConverter = catModelConverter;
            _subcatModelConverter = subcatModelConverter;
        }

        public FileEditModel BuildModel(FileEditModel model)
        {
            var catRepository = _unitOfWork.GetRepository<Domain.DocumentCategory>();
            var subcatRepository = _unitOfWork.GetRepository<DocumentSubcategory>();

            var cats = catRepository.Get();
            var subcats = subcatRepository.Get();

            model.Categories = cats.Select(x => _catModelConverter.ConvertToModel(x)).ToList();
            model.Subcategories = subcats.Select(x => _subcatModelConverter.ConvertToModel(x)).ToList();

            return model;
        }
    }
}

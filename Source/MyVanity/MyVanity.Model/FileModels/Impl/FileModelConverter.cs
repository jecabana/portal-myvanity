using System;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.DocumentCategoryModels.Impl;

namespace MyVanity.Model.FileModels.Impl
{
    public class FileModelConverter<TDocument> : IModelConverter<TDocument, FileEditModel> where TDocument : Document
    {
        private readonly IUnitOfWork _unitOfWork;

        public FileModelConverter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FileEditModel ConvertToModel(TDocument entity)
        {
            var model = new FileEditModel
                   {
                       SelectedCategory = entity.CategoryId,
                       Censured = entity.Censured,
                       ContentType = entity.ContentType,
                       Name = entity.Name,
                       Description = entity.Description,
                       Id = entity.Id,
                       Path = entity.Path,
                       SelectedSubcategory = entity.SubcategoryId,
                       Category = new CategoryEditModel { Id = entity.CategoryId, Name = entity.Category.Name },
                       Subcategory = entity.Subcategory == null ? null : new CategoryEditModel { Id = entity.Subcategory.Id, Name = entity.Subcategory.Name }
                   };

            return model;
        }

        public TDocument ConvertToSource(FileEditModel model)
        {
            TDocument document;

            if (model.Id != 0)
            {
                var repository = _unitOfWork.GetRepository<TDocument>();
                document = repository.FindById(model.Id);
            }
            else
                document = Activator.CreateInstance<TDocument>();

            document = ModelConverterHelper.CopyObjectProperties(model, document);

            var categoryRepository = _unitOfWork.GetRepository<DocumentCategory>();
            document.Category = categoryRepository.FindById(model.SelectedCategory);

            if (model.SelectedSubcategory != null)
            {
                var subcategoryRepository = _unitOfWork.GetRepository<DocumentSubcategory>();
                document.Subcategory = subcategoryRepository.FindById(model.SelectedSubcategory.Value);
            }

            document.ContentType = Helpers.ResolveContentTypeFromName(model.RealName);
            return document;
        }
    }
}

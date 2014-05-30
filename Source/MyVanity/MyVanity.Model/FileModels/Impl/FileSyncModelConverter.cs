using System;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.DocumentCategoryModels.Impl;

namespace MyVanity.Model.FileModels.Impl
{
    public class FileSyncModelConverter : IModelConverter<SharedDocument, FileEditModelSync>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FileSyncModelConverter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FileEditModelSync ConvertToModel(SharedDocument entity)
        {
            var model = ModelConverterHelper.CopyObjectProperties(entity, new FileEditModelSync());

            model.SelectedCategory = entity.CategoryId;
            model.SelectedSubcategory = entity.SubcategoryId;
            model.Category = new CategoryEditModel {Id = entity.CategoryId, Name = entity.Category.Name};
            model.Subcategory = entity.Subcategory == null
                ? null
                : new CategoryEditModel {Id = entity.Subcategory.Id, Name = entity.Subcategory.Name};
            
            return model;
        }

        public SharedDocument ConvertToSource(FileEditModelSync model)
        {
            SharedDocument document;

            if (model.Id != 0)
            {
                var repository = _unitOfWork.GetRepository<SharedDocument>();
                document = repository.FindById(model.Id);
            }
            else
                document = Activator.CreateInstance<SharedDocument>();
            
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

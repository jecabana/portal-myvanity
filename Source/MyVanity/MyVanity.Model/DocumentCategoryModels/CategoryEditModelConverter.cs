using MyVanity.Model.DocumentCategoryModels.Impl;

namespace MyVanity.Model.DocumentCategoryModels
{
    public class CategoryEditModelConverter : IEntityConverter<Domain.DocumentCategory, CategoryEditModel>
    {
        public CategoryEditModel ConvertToModel(Domain.DocumentCategory entity)
        {
            return new CategoryEditModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }

    public class SubcategoryEditModelConverter : IEntityConverter<Domain.DocumentSubcategory, CategoryEditModel>
    {
        public CategoryEditModel ConvertToModel(Domain.DocumentSubcategory entity)
        {
            return new CategoryEditModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}

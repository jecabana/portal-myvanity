using MyVanity.Model.ProcedureCategory;

namespace MyVanity.Model.ProcedureCategoryModels.Impl
{
    public class ProcedureCategoryModelConverter : IProcedureCategoryModelConverter
    {
        public ProcedureCategoryViewModel ConvertToModel(Domain.ProcedureCategory entity)
        {
            return new ProcedureCategoryViewModel { Id = entity.Id, Name = entity.Name };
        }

        public Domain.ProcedureCategory ConvertToSource(ProcedureCategoryViewModel model)
        {
            return new Domain.ProcedureCategory{ Id = model.Id, Name = model.Name };
        }
    }
}

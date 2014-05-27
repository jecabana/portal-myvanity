namespace MyVanity.Model.ProcedureTypeModels.Impl
{
    public class ProcedureTypeModelConverter : IModelConverter<Domain.ProcedureType, ProcedureTypeEditModel>
    {
        public ProcedureTypeEditModel ConvertToModel(Domain.ProcedureType entity)
        {
            return new ProcedureTypeEditModel{ Id = entity.Id, Name = entity.Name };
        }

        public Domain.ProcedureType ConvertToSource(ProcedureTypeEditModel model)
        {
            return new Domain.ProcedureType{ Id = model.Id, Name = model.Name };
        }
    }
}

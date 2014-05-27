
using MyVanity.Model.ProcedureModels.Impl;

namespace MyVanity.Model.ProcedureModels
{
    public interface IProcedureModelConverter : IModelConverter<Domain.Procedure, ProcedureEditModel>, IViewModelBuilder<ProcedureEditModel>
    {
    }
}

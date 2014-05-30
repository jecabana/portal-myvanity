using MyVanity.Model.PatientProcedureModels.Impl;

namespace MyVanity.Model.PatientProcedureModels
{
    public interface IPatientProcedureModelConverter : IModelConverter<Domain.UserProcedure, PatientProcedureEditModel>, IViewModelBuilder<PatientProcedureEditModel>
    { }
}

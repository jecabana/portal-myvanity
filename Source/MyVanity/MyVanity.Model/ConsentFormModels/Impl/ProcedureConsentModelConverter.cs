using MyVanity.Domain;

namespace MyVanity.Model.ConsentFormModels.Impl
{
    public class ProcedureConsentModelConverter : IModelConverter<UserProcedureConsentSign, ProcedureConsentViewModel>
    {
        public UserProcedureConsentSign ConvertToSource(ProcedureConsentViewModel model)
        {
            return new UserProcedureConsentSign
                   {
                       Id = model.Id,
                       ConsentFormId = model.ConsentId,
                       Signed = model.Signed,
                       UserProcedureId = model.ProcedureId
                   };
        }

        public ProcedureConsentViewModel ConvertToModel(UserProcedureConsentSign entity)
        {
            return new ProcedureConsentViewModel
                   {
                       ConsentId = entity.ConsentFormId,
                       Id = entity.Id,
                       ProcedureId = entity.UserProcedureId,
                       Signed = entity.Signed,
                       ConsentTitle = entity.ConsentForm.Title,
                       Body = entity.ConsentForm.Description
                   };
        }
    }
}

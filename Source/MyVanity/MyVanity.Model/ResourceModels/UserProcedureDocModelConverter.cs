using System;
using MyVanity.Domain;
using MyVanity.Model.FileModels.Impl;

namespace MyVanity.Model.ResourceModels
{
    public class UserProcedureDocModelConverter : IModelConverter<UserProcedurePatientDocument, UserProcedurePatientDocViewModel>
    {
        private readonly IModelConverter<UserProcedurePatientDocument, FileEditModel> _baseModelConverter; 

        public UserProcedureDocModelConverter(IModelConverter<UserProcedurePatientDocument, FileEditModel> baseModelConverter)
        {
            _baseModelConverter = baseModelConverter;
        }

        public UserProcedurePatientDocViewModel ConvertToModel(UserProcedurePatientDocument entity)
        {
            var baseModel = _baseModelConverter.ConvertToModel(entity);

            var model = new UserProcedurePatientDocViewModel
                        {
                            Category = baseModel.Category,
                            Categories = baseModel.Categories,
                            Censured = baseModel.Censured,
                            ContentType = baseModel.ContentType,
                            Description = baseModel.Description,
                            Id = baseModel.Id,
                            Name = baseModel.Name,
                            Path = baseModel.Path,
                            RealName = baseModel.RealName,
                            SelectedCategory = baseModel.SelectedCategory,
                            SelectedSubcategory = baseModel.SelectedSubcategory,
                            Subcategories = baseModel.Subcategories,
                            Subcategory = baseModel.Subcategory,
                            PatientName = string.Format("{0} {1}", entity.Patient.Profile.FirstName, entity.Patient.Profile.MiddleName),
                            ProcedureIdentifier = string.Format("{0} {1}", entity.UserProcedure.Procedure.Category.Name, entity.UserProcedure.Procedure.Type.Name)
                        };

            return model;
        }

        public UserProcedurePatientDocument ConvertToSource(UserProcedurePatientDocViewModel model)
        {
            throw new NotSupportedException();
        }
    }
}

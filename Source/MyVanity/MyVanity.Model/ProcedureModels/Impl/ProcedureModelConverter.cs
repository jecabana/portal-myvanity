using System;
using System.Linq;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.ProcedureCategoryModels.Impl;
using MyVanity.Model.ProcedureTypeModels.Impl;

namespace MyVanity.Model.ProcedureModels.Impl
{
    public class ProcedureModelConverter : IProcedureModelConverter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModelConverter<Domain.ProcedureCategory, ProcedureCategoryViewModel> _categoryModelConverter;
        private readonly IModelConverter<ProcedureType, ProcedureTypeEditModel> _typesModelConverter;

        public ProcedureModelConverter(IUnitOfWork unitOfWork, 
                                       IModelConverter<ProcedureType, ProcedureTypeEditModel> typesModelConverter, 
                                       IModelConverter<Domain.ProcedureCategory, ProcedureCategoryViewModel> categoryModelConverter)
        {
            _unitOfWork = unitOfWork;
            _typesModelConverter = typesModelConverter;
            _categoryModelConverter = categoryModelConverter;
        }

        public ProcedureEditModel ConvertToModel(Procedure entity)
        {
            var model = BuildModel(new ProcedureEditModel());
            model.Id = entity.Id;
            model.Description = entity.Description;
            model.RegularPrice = entity.RegularPrice;
            model.SalePrice = entity.SalePrice;
            model.SelectedCategoryId = entity.CategoryId;
            model.SelectedTypeId = entity.TypeId;
            model.PicturePath = entity.PicPath;

            return model;
        }

        public Procedure ConvertToSource(ProcedureEditModel model)
        {
            var procedure = model.Id != 0 ? _unitOfWork.GetRepository<Procedure>().FindById(model.Id) 
                                          : new Procedure();

            procedure.Category = _unitOfWork.GetRepository<Domain.ProcedureCategory>().FindById(model.SelectedCategoryId);
            procedure.Type = _unitOfWork.GetRepository<ProcedureType>().FindById(model.SelectedTypeId);
            procedure.Description = model.Description;
            procedure.RegularPrice = model.RegularPrice;
            procedure.SalePrice = model.SalePrice;
            procedure.CategoryId = model.SelectedCategoryId;
            procedure.TypeId = model.SelectedTypeId;
            procedure.PicPath = model.PicturePath;
            return procedure;
        }

        public ProcedureEditModel BuildModel(ProcedureEditModel model)
        {
            var categories = _unitOfWork.GetRepository<Domain.ProcedureCategory>().Get();
            model.Categories = categories.Select(x => _categoryModelConverter.ConvertToModel(x)).ToList();

            var categoryTypes = _unitOfWork.GetRepository<ProcedureType>().Get();
            model.Types = categoryTypes.Select(x => _typesModelConverter.ConvertToModel(x)).ToList();

            return model;
        }
    }
}


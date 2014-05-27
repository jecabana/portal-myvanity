using MyVanity.Domain;
using MyVanity.Domain.UoW;

namespace MyVanity.Model.ConsentFormModels.Impl
{
    public class ConsentFormModelConverter : IModelConverter<ConsentForm, ConsentFormEditModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsentFormModelConverter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ConsentFormEditModel ConvertToModel(ConsentForm entity)
        {
            var model = new ConsentFormEditModel
                   {
                       Description = entity.Description,
                       FileGuid = entity.FilePath,
                       Id = entity.Id,
                       Title = entity.Title
                   };

            return model;
        }

        public ConsentForm ConvertToSource(ConsentFormEditModel model)
        {
            ConsentForm consent;

            if (model.Id != 0)
            {
                var repository = _unitOfWork.GetRepository<ConsentForm>();
                consent = repository.FindById(model.Id);
            }
            else consent  = new ConsentForm();

            consent.Description = model.Description;
            consent.Title = model.Title;
            consent.FilePath = model.FileGuid;

            return consent;
        }
    }
}

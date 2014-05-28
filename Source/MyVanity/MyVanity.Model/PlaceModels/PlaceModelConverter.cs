using MyVanity.Domain;
using MyVanity.Domain.UoW;

namespace MyVanity.Model.PlaceModels
{
    public class PlaceModelConverter : IModelConverter<Place,PlaceEditModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlaceModelConverter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PlaceEditModel ConvertToModel(Place entity)
        {
            return ModelConverterHelper.CopyObjectProperties(entity, new PlaceEditModel());
        }

        public Place ConvertToSource(PlaceEditModel model)
        {
            var place = model.Id != 0 ? _unitOfWork.GetRepository<Place>().FindById(model.Id) : new Place();
            return ModelConverterHelper.CopyObjectProperties(model, place);
        }
    }
}

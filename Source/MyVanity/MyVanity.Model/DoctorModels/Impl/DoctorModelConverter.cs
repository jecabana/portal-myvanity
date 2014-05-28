using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.ProfileModels.Profile;

namespace MyVanity.Model.DoctorModels.Impl
{
    public class DoctorModelConverter : IDoctorViewModelConverter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProfileModelConverter _profileModelConverter;

        public DoctorModelConverter(IUnitOfWork unitOfWork, IProfileModelConverter profileModelConverter)
        {
            _unitOfWork = unitOfWork;
            _profileModelConverter = profileModelConverter;
        }

        public DoctorEditModel ConvertToModel(Doctor entity)
        {
            var model = ModelConverterHelper.CopyObjectProperties(entity, new DoctorEditModel());
            model.Profile = _profileModelConverter.ConvertToModel(entity.PersonDetails);
            return model;
        }

        public Doctor ConvertToSource(DoctorEditModel model)
        {
            var doctor = model.Id != 0 ? _unitOfWork.GetRepository<Doctor>().FindById(model.Id) 
                                       : new Doctor();

            doctor = ModelConverterHelper.CopyObjectProperties(model, doctor);
            doctor.PersonDetails = _profileModelConverter.ConvertToSource(model.Profile);
            return doctor;
        }
    }
}

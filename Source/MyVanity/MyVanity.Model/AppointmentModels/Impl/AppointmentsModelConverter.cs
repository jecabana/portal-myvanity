using System;
using System.Linq;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.PlaceModels;

namespace MyVanity.Model.AppointmentModels.Impl
{
    public class AppointmentsModelConverter: IAppointmentModelConverter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModelConverter<Place, PlaceEditModel> _placeModelConverter; 

        public AppointmentsModelConverter(IUnitOfWork unitOfWork, IModelConverter<Place, PlaceEditModel> placeModelConverter)
        {
            _unitOfWork = unitOfWork;
            _placeModelConverter = placeModelConverter;
        }

        public AppointmentEditModel ConvertToModel(Appointment entity)
        {
            var model = new AppointmentEditModel
                   {
                       Date = entity.Date,
                       Description = entity.Description,
                       Id = entity.Id,
                       ProcedureId = entity.UserProcedureId,
                       Status = Enum.GetName(typeof(AppointmentStatus), entity.Status),
                       StatusEnum = entity.Status,
                       ProcedureIdentifier = string.Format("{0} {1}, ({2},{3})", entity.Procedure.Patient.Profile.FirstName
                                                                               , entity.Procedure.Patient.Profile.MiddleName
                                                                               , entity.Procedure.Procedure.Category.Name
                                                                               , entity.Procedure.Procedure.Type.Name)
                   };

            var placesRepository = _unitOfWork.GetRepository<Place>();
            model.Places = placesRepository.Get().Select(x => _placeModelConverter.ConvertToModel(x)).ToList();
            model.SelectedPlace = entity.PlaceId;

            return BuildModel(model);
        }

        public Appointment ConvertToSource(AppointmentEditModel model)
        {
            var appointment = model.Id != 0
                                       ? _unitOfWork.GetRepository<Appointment>().FindById(model.Id)
                                       : new Appointment();

            appointment.Date = model.Date.Value;
            appointment.UserProcedureId = model.ProcedureId.Value;
            appointment.Description = model.Description;
            appointment.Id = model.Id;
            appointment.Procedure = null;
            appointment.Description = model.Description;
            appointment.PlaceId = model.SelectedPlace;
            return appointment;
        }

        public AppointmentEditModel BuildModel(AppointmentEditModel model)
        {
            var placesRepository = _unitOfWork.GetRepository<Place>();
            model.Places = placesRepository.Get().Select(x => _placeModelConverter.ConvertToModel(x)).ToList();
            return model;
        }
    }
}

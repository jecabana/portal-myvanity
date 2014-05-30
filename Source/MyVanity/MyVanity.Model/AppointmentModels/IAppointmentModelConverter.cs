using MyVanity.Domain;
using MyVanity.Model.AppointmentModels.Impl;

namespace MyVanity.Model.AppointmentModels
{
    public interface IAppointmentModelConverter : IModelConverter<Appointment, AppointmentEditModel>, IViewModelBuilder<AppointmentEditModel>
    {
    }
}

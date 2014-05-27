using System.Collections.Generic;

namespace MyVanity.Model.AppointmentModels.Impl
{
    public class AppointmentsIndexModel
    {
        public AppointmentsIndexModel(IEnumerable<AppointmentEditModel> appointments)
        {
            Appointments = appointments;
        }

        public IEnumerable<AppointmentEditModel> Appointments { get; set; }
    }
}

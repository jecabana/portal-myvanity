using System;
using System.ComponentModel.DataAnnotations;

namespace MyVanity.Model.AppointmentModels.Impl
{
    public class AppointmentReportViewModel
    {
        public string Patient { get; set; }

        [Display(Name = "Details")]
        public string AppointmentDescription { get; set; }

        [Display(Name = "Patient Number")]
        public int PatientNumber { get; set; }

        [Display(Name = "Missing Days")]
        public int MissingDays 
        {
            get { return AppointmentDate.Day - DateTime.Now.Day; }
        }

        public bool Warning 
        {
            get { return MissingDays == 3; }
        }

        public bool Danger
        {
            get { return MissingDays <= 2; }
        }

        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }
    }
}

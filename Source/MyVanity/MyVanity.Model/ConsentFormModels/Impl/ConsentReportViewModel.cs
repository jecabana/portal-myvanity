using System;
using System.ComponentModel.DataAnnotations;

namespace MyVanity.Model.ConsentFormModels.Impl
{
    public class ConsentReportViewModel
    {
        [Display(Name = "Procedure Date")]
        public DateTime ProcedureDate { get; set; }

        [Display(Name = "Missing Days")]
        public int MissingDays 
        {
            get { return ProcedureDate.Day - DateTime.Now.Day; }
        }

        [Display(Name = "Patient")]
        public string PatientName { get; set; }

        [Display(Name = "Patient Number")]
        public string PatientNumber { get; set; }

        [Display(Name = "Procedure Name")]
        public string ProcedureName { get; set; }

        [Display(Name = "Consent")]
        public string ConsentTitle { get; set; }

        public bool Warning
        {
            get { return MissingDays == 3; }
        }

        public bool Danger
        {
            get { return MissingDays <= 2; }
        }
    }
}

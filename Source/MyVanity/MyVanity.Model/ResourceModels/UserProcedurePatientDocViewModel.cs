using System.ComponentModel.DataAnnotations;
using MyVanity.Model.FileModels.Impl;

namespace MyVanity.Model.ResourceModels
{
    public class UserProcedurePatientDocViewModel : FileEditModel
    {
        [Display(Name = "Patient")]
        public string PatientName { get; set; }

        [Display(Name = "Procedure")]
        public string ProcedureIdentifier { get; set; }
    }
}

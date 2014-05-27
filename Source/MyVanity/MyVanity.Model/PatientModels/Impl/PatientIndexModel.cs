using System.Collections.Generic;

namespace MyVanity.Model.PatientModels.Impl
{
    public class PatientIndexModel
    {
        public PatientIndexModel(IEnumerable<PatientEditModel> patients)
        {
            Patients = patients;
        }

        public IEnumerable<PatientEditModel> Patients { get; set; }
    }
}

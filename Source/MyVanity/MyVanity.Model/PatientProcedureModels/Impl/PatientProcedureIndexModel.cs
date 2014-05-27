using System.Collections.Generic;

namespace MyVanity.Model.PatientProcedureModels.Impl
{
    public class PatientProcedureIndexModel
    {
        public PatientProcedureIndexModel(IEnumerable<PatientProcedureEditModel> procedures)
        {
            Procedures = procedures;
        }

        public IEnumerable<PatientProcedureEditModel> Procedures { get; set; }
    }
}

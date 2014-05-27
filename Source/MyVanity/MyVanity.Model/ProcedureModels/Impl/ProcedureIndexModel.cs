using System.Collections.Generic;

namespace MyVanity.Model.ProcedureModels.Impl
{
    public class ProcedureIndexModel
    {
        public ProcedureIndexModel(IEnumerable<ProcedureEditModel> procedures)
        {
            Procedures = procedures;
        }

        public IEnumerable<ProcedureEditModel> Procedures { get; set; }
    }
}

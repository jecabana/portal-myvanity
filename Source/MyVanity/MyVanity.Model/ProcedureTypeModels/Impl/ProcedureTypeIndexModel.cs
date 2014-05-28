using System.Collections.Generic;

namespace MyVanity.Model.ProcedureTypeModels.Impl
{
    public class ProcedureTypeIndexModel
    {
        public ProcedureTypeIndexModel(IEnumerable<ProcedureTypeEditModel> procedureTypes)
        {
            ProcedureTypes = procedureTypes;
        }

        public IEnumerable<ProcedureTypeEditModel> ProcedureTypes { get; set; }
    }
}

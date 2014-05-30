using System.Collections.Generic;
using MyVanity.Model.Pager;
using MyVanity.Model.Results;

namespace MyVanity.Model.PatientProcedureModels.Impl
{
    public class PatientProcedureIndexModel : PagedViewModel<PatientProcedureEditModel>
    {
        public PatientProcedureIndexModel(PagedResult<IEnumerable<PatientProcedureEditModel>> results)
        {
            Items = results.Result;

            Page = results.PageNumber;
            PageSize = results.PageSize;
            TotalPages = results.TotalPages;
            TotalRecords = results.TotalItems;
            SortMode = results.SortOrder;
            OrderColumn = results.SortColumn;
        }
    }
}

using System.Collections.Generic;
using MyVanity.Model.Pager;
using MyVanity.Model.Results;

namespace MyVanity.Model.ProcedureModels.Impl
{
    public class ProcedureIndexModel : PagedViewModel<ProcedureEditModel>
    {
        public ProcedureIndexModel(PagedResult<IEnumerable<ProcedureEditModel>> results)
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

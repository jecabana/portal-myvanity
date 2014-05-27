using System.Collections.Generic;
using MyVanity.Model.Pager;
using MyVanity.Model.Results;

namespace MyVanity.Model.PatientModels.Impl
{
    public class PatientIndexModel : PagedViewModel<PatientEditModel>
    {
        public PatientIndexModel(PagedResult<IEnumerable<PatientEditModel>> results)
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

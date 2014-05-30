using System.Collections.Generic;
using MyVanity.Model.Pager;
using MyVanity.Model.Results;

namespace MyVanity.Model.AppointmentModels.Impl
{
    public class AppointmentsIndexModel : PagedViewModel<AppointmentEditModel>
    {
        public AppointmentsIndexModel(PagedResult<IEnumerable<AppointmentEditModel>> results)
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

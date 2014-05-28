using System.Collections.Generic;
using MyVanity.Model.Pager;
using MyVanity.Model.Results;

namespace MyVanity.Model.MessageModels
{
    public class MessageIndexModel : PagedViewModel<MessageEditModel>
    {
        public MessageIndexModel(PagedResult<IEnumerable<MessageEditModel>> results)
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

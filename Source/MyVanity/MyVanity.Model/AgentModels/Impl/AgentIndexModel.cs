﻿using System.Collections.Generic;
using MyVanity.Model.Pager;
using MyVanity.Model.Results;

namespace  MyVanity.Model.AgentModels.Impl
{
    public class AgentIndexModel : PagedViewModel<AgentEditModel>
    {
        public AgentIndexModel(PagedResult<IEnumerable<AgentEditModel>> results)
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

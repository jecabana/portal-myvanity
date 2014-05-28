using System.Collections.Generic;
using MyVanity.Common;

namespace MyVanity.Model.Pager
{
    public class PagedViewModel<T> : IPagedViewModel
    {
        public int Page { get; set; }
        
        public int TotalPages { get; set; }
        
        public int PageSize { get; set; }
        
        public int TotalRecords { get; set; }
        
        public string OrderColumn { get; set; }

        public SortMode SortMode { get; set; }
        
        public IEnumerable<T> Items { get; set; }
    }
}

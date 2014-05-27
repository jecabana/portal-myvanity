using MyVanity.Common;

namespace MyVanity.Views.Filters
{
    /// <summary>
    /// Represents the sorting/paging information sent to a service returning multiple results
    /// </summary>
    public class FilterInformation
    {
        /// <summary>
        /// Size of the pages being returned
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Current page trying to fetch
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Column to use to order by
        /// </summary>
        public string OrderColumn { get; set; }

        /// <summary>
        /// Mode to sort
        /// </summary>
        public SortMode SortMode { get; set; }

        public FilterInformation()
            : this(10, 0)
        {

        }

        public FilterInformation(int pageSize, int pageNumb)
        {
            PageSize = pageSize;
            Page = pageNumb;
        }

        public static FilterInformation AllItemsNoSort
        {
            get
            {
                return new FilterInformation(0, 0);
            }
        }
    }
}

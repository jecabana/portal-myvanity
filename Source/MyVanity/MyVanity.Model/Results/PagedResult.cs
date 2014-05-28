using MyVanity.Common;

namespace MyVanity.Model.Results
{
    public class PagedResult<T> : BaseResult<T>
    {
        public int PageSize { get; private set; }

        public int PageNumber { get; private set; }

        public int TotalItems { get; private set; }

        public int TotalPages { get; private set; }

        public string SortColumn { get; set; }

        public SortMode SortOrder { get; set; }

        public bool PreviousIsVisible
        {
            get { return PageNumber > 0; }
        }

        public bool NextIsVisible
        {
            get { return PageNumber < TotalPages; }
        }

        public PagedResult(int pageSize, int pageNumber, int totalItems, int totalPages)
        {
            TotalPages = totalPages;
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}

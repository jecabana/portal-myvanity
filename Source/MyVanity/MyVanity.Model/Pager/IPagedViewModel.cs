using MyVanity.Common;

namespace MyVanity.Model
{
    public interface IPagedViewModel
    {
        int Page { get; set; }
        int TotalPages { get; set; }
        int PageSize { get; set; }
        int TotalRecords { get; set; }
        string OrderColumn { get; set; }
        SortMode SortMode { get; set; }
    }
}

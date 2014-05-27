using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using MyVanity.Common;
using MyVanity.Model;
using MyVanity.Views.Filters;

namespace MyVanity.Web.MvcHelpers
{
    public static class PagerExtension
    {
        public static MvcHtmlString SortColumn(this HtmlHelper helper, IPagedViewModel pageModel, string field, string caption, string controller, string view)
        {
            //Html.ActionLink("Member/Prospect ID", "Index", "Leads", new { OrderColumn = "nisamemberid", SortMode = sMode }, new { data_sort_by = "nisamemberid", direction = "asc" })
            var sortOrder = pageModel.SortMode == SortMode.Ascending ? "Descending" : "Ascending";

            var attributes = new RouteValueDictionary();
            attributes.Add("SortMode", sortOrder);
            attributes.Add("OrderColumn", field);
            attributes.Add("Page", pageModel.Page);
            attributes.Add("PageSize", pageModel.PageSize);

            var link = helper.ActionLink(caption, view, controller, attributes, null);

            return new MvcHtmlString(link.ToString());
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedViewModel pageModel, string controller, string view)
        {
            var options = new[] {
                new Tuple<int, string>(10, "10"),
                new Tuple<int, string>(20, "20"),
                new Tuple<int, string>(50, "50"),
                new Tuple<int, string>(0, "All"),
            };
            var sb = new StringBuilder();

            sb.AppendLine("<div class=\"paging\" id=\"pager\">");
            sb.AppendLine("<div class=\"resultsPerPage\">");
            sb.AppendLine("<span>Results per page: </span>");

            sb.AppendLine("<select id=\"pageSelector\">");

            for (var i = 0; i < options.Length; i++)
                sb.AppendFormat("<option value=\"{0}\"> {1} </option>", options[i].Item1, options[i].Item2);

            sb.AppendLine("</select>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div class=\"pagingDesc\">");
            sb.AppendFormat("Page {0} of {1}", pageModel.Page + 1, pageModel.TotalPages + 1);
            sb.AppendLine("</div>");

            sb.AppendLine("<ul class=\"pagination\">");
            for (var i = 0; i < pageModel.TotalPages + 1; i++)
            {
                var number = i.ToString();
                var page = (i + 1).ToString();
                var selected = i == pageModel.Page;
                var selectedClass = selected ? " class=\"active\"" : string.Empty;

                var button = helper.ActionLink(page, view, controller, new { pageSize = pageModel.PageSize, page = i }, new { data_page = number });
                sb.AppendLine("<li" + selectedClass + ">");
                sb.AppendLine(button.ToHtmlString());
                sb.AppendLine("</li>");
            }
            sb.AppendLine("</ul>");

            sb.AppendLine("</div>");

            return new MvcHtmlString(sb.ToString());
        }
    }
}
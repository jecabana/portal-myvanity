using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using MyVanity.Common;
using MyVanity.Model.Pager;

namespace MyVanity.Web.MvcHelpers
{
    public static class PagerExtension
    {
        public static MvcHtmlString SortColumn(this HtmlHelper helper, IPagedViewModel pageModel, string field, string caption, string controller, string view)
        {
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
            if (pageModel.TotalRecords <= pageModel.PageSize)
             return null;

            var options = new[] {
                new Tuple<int, string>(25, "25"),
                new Tuple<int, string>(50, "50"),
                new Tuple<int, string>(75, "75"),
                new Tuple<int, string>(100, "100"),
                new Tuple<int, string>(1000, "1000"),
            };
            var sb = new StringBuilder();

            const string liDisabled = "<li class=\"disabled\">";
            const string liActive = "<li class=\"active\">";
            const string li = "<li>";
            const string liClose = "</li>";

            sb.AppendLine("<div class=\"paging\" id=\"pager\">");
            sb.AppendLine("<div class=\"resultsPerPage\">");
            sb.AppendLine("<span>Results per page: </span>");

            sb.AppendLine("<select id=\"pageSelector\">");

            for (var i = 0; i < options.Length; i++)
                sb.AppendFormat("<option value=\"{0}\"> {1} </option>", options[i].Item1, options[i].Item2);

            sb.AppendLine("</select>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div class=\"pagingDesc\">");
            sb.AppendFormat("Page {0} of {1}", pageModel.Page + 1, pageModel.TotalPages);
            sb.AppendLine("</div>");

            sb.AppendLine("<ul class=\"pagination\">");

            var bounds = CalculatePagerBounds(pageModel);

            var isDisabled = (pageModel.Page == 0);
            var opening = isDisabled ? liDisabled : li;

            sb.AppendLine(opening);
                var first = helper.CreateLinkButton(pageModel, 0, view, controller, null, "First Page");
                sb.AppendLine(first.ToHtmlString());
            sb.AppendLine(liClose);

            sb.AppendLine(opening);
                var prevPage = (pageModel.Page - 1) >= 0 ? (pageModel.Page - 1) : 0;
                var prev = helper.CreateLinkButton(pageModel,  prevPage, view, controller, null, "&laquo;");  
                sb.AppendLine(prev.ToHtmlString());
            sb.AppendLine(liClose);

            for (var i = bounds.Item1; i <= bounds.Item2; i++)
            {
                var selected = i == pageModel.Page;
                opening = selected ? liActive : li;
                
                sb.AppendLine(opening);
                    var button = helper.CreateLinkButton(pageModel, i, view, controller, null);
                    sb.AppendLine(button.ToHtmlString());
                sb.AppendLine(liClose);
            }

            opening = (pageModel.Page == pageModel.TotalPages - 1) ? liDisabled : li;

            sb.AppendLine(opening);
                var nextPage = pageModel.Page + 1 <= pageModel.TotalPages ? pageModel.Page + 1 : pageModel.TotalPages - 1;
                var next = helper.CreateLinkButton(pageModel, nextPage, view, controller, null, "&raquo;");
                sb.AppendLine(next.ToHtmlString());
            sb.AppendLine(liClose);

            sb.AppendLine(opening);
                var last = helper.CreateLinkButton(pageModel, pageModel.TotalPages - 1, view, controller, "pagingButton", "Last Page");
                sb.AppendLine(last.ToHtmlString());
            sb.AppendLine(liClose);

            sb.AppendLine("</ul>");
            sb.AppendLine("</div>");

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString SmallPager(this HtmlHelper helper, IPagedViewModel pageModel, string controller, string view)
        {
            if (pageModel.TotalRecords <= pageModel.PageSize)
                return null;

            var sb = new StringBuilder();

            const string liDisabled = "<li class=\"disabled\">";
            const string liActive = "<li class=\"active\">";
            const string li = "<li>";
            const string liClose = "</li>";

            sb.AppendLine("<div class=\"paging\" id=\"pager\">");
            sb.AppendLine("<ul class=\"pagination pagination-sm\">");

            var bounds = CalculatePagerBounds(pageModel);

            var isDisabled = (pageModel.Page == 0);
            var opening = isDisabled ? liDisabled : li;

            sb.AppendLine(opening);
            var first = helper.CreateLinkButton(pageModel, 0, view, controller, null, "First Page");
            sb.AppendLine(first.ToHtmlString());
            sb.AppendLine(liClose);

            sb.AppendLine(opening);
            var prevPage = (pageModel.Page - 1) >= 0 ? (pageModel.Page - 1) : 0;
            var prev = helper.CreateLinkButton(pageModel, prevPage, view, controller, null, "&laquo;");
            sb.AppendLine(prev.ToHtmlString());
            sb.AppendLine(liClose);

            for (var i = bounds.Item1; i <= bounds.Item2; i++)
            {
                var selected = i == pageModel.Page;
                opening = selected ? liActive : li;

                sb.AppendLine(opening);
                var button = helper.CreateLinkButton(pageModel, i, view, controller, null);
                sb.AppendLine(button.ToHtmlString());
                sb.AppendLine(liClose);
            }

            opening = (pageModel.Page == pageModel.TotalPages - 1) ? liDisabled : li;

            sb.AppendLine(opening);
            var nextPage = pageModel.Page + 1 <= pageModel.TotalPages ? pageModel.Page + 1 : pageModel.TotalPages - 1;
            var next = helper.CreateLinkButton(pageModel, nextPage, view, controller, null, "&raquo;");
            sb.AppendLine(next.ToHtmlString());
            sb.AppendLine(liClose);

            sb.AppendLine(opening);
            var last = helper.CreateLinkButton(pageModel, pageModel.TotalPages - 1, view, controller, "pagingButton", "Last Page");
            sb.AppendLine(last.ToHtmlString());
            sb.AppendLine(liClose);

            sb.AppendLine("</ul>");
            sb.AppendLine("</div>");

            return new MvcHtmlString(sb.ToString());
        }

        private static MvcHtmlString CreateLinkButton(this HtmlHelper helper, IPagedViewModel pageModel, int pageNumber, string view, string controller, string itemClass = null, string title = null)
        {
            var page = string.IsNullOrEmpty(title) ? (pageNumber + 1).ToString(CultureInfo.InvariantCulture) : WebUtility.HtmlDecode(title);
            var number = pageNumber.ToString(CultureInfo.InvariantCulture);

            return helper.ActionLink(page, view, controller, new { pageModel.PageSize, Page = pageNumber }, new { @class = itemClass, data_role = "pager-button", data_page = number });
        }

        private static Tuple<int, int> CalculatePagerBounds(IPagedViewModel pageModel)
        {
            var start = pageModel.Page - 9;

            if (start < 0)
            {
                start = 0;
            }

            var end = Math.Max(19, pageModel.Page + 10);
            end = Math.Min(end, pageModel.TotalPages - 1);

            if (end - start != 19)
            {
                if (end < pageModel.TotalPages)
                {
                    start = Math.Max(0, end - 19);
                }
            }

            return new Tuple<int, int>(start, end);
        }
    }
}
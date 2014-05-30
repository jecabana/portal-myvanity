(function ($) {
    $.fn.pager = function (options) {
        var pager = $.fn.pager;

        pager.options = options;

        pager.init = function () {
            var pageSelector = $(pager.options.pageSelector);
            pageSelector.change(pager.pageChanged);
            pager.alterPagingButtons();

            $("option", pageSelector).filter(function () {
                return $(this).val() == pager.options.pageSize;
            }).prop('selected', true);

            $(options.buttonSelector + ".disabled").click(function() {
                return false;
            });
        };

        pager.alterPagingButtons = function () {
            var pagerButtons = $("ul.pagination li a");
            pagerButtons.each(pager.pagerButtonIterator);
        };

        pager.pagerButtonIterator = function (index, val) {
            var currentUrl = $(val).attr("href");
            var args = pager.loadUrlArguments();
            currentUrl += args;

            $(val).attr("href", currentUrl);
        };

        pager.pageChanged = function () {
            var pageSelector = $(pager.options.pageSelector);
            var selectedIndex = pageSelector[0].selectedIndex;

            var option = pageSelector[0].options[selectedIndex];

            var currentUrl = window.location;
            var href = currentUrl.href.replace(currentUrl.search, "");
            var args = pager.loadUrlArguments();

            var search = href + "?PageSize=" + option.value + "&Page=0" + args;
            window.location = search;
        };

        pager.loadUrlArguments = function () {
            var currentUrl = window.location;
            var query = currentUrl.search;

            var splittedQuery = query.split("&");
            var newComposedQuery = "";

            for (var i = 0; i < splittedQuery.length; i++) {
                var queryPiece = splittedQuery[i];

                if (queryPiece.indexOf("PageSize") != -1 || queryPiece.indexOf("Page") != -1 || queryPiece.indexOf("OrderColumn") != -1 || queryPiece.indexOf("SortMode") != -1)
                    continue;

                newComposedQuery += queryPiece;

                if (i + 1 < splittedQuery.length)
                    newComposedQuery += "&";
            }

            if (newComposedQuery.length > 1)
                newComposedQuery = "&" + newComposedQuery;

            return newComposedQuery;
        };

        pager.init();
        return pager;
    };

}(jQuery));
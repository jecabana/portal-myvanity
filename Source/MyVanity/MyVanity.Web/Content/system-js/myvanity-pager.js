(function ($) {
    $.fn.pager = function (options) {
        var pager = $.fn.pager;

        pager.options = options;

        pager.init = function () {
            var pageSelector = $(pager.options.pageSelector);
            pageSelector.change(pager.pageChanged);

            $("option", pageSelector).filter(function () {
                return $(this).val() == pager.options.pageSize;
            }).prop('selected', true);

        };

        pager.pageChanged = function () {
            var pageSelector = $(pager.options.pageSelector);
            var selectedIndex = pageSelector[0].selectedIndex;

            var option = pageSelector[0].options[selectedIndex];

            var currentUrl = window.location;
            var href = currentUrl.href.replace(currentUrl.search, "");

            var search = href + "?size=" + option.value + "&page=0";
            window.location = search;
        };

        pager.init();
        return pager;
    };

}(jQuery));
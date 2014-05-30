(function ($) {
    $.fn.vanityTab = function (options) {
        var self = this;

        this.defaults = {
            tabContainer: "div.tabsWrapper",
            tabs: "ul.tabs li",
            tabMenuItem: "ul.tabs li a",
            tabContentItem: "div.tabContent div.tab"
        };

        var pluginOptions = $.extend({}, this.defaults, options);
        this.options = pluginOptions;

        this.tabClicked = function () {
            var parent = $(this).parent();
            if (parent.hasClass("disable")) {
                return false;
            }

            var tabId = $(this).attr("href");
            var selectedTab = $(tabId);

            $(self.options.tabs).removeClass("active");
            $($(this).parent().parent()).addClass("active");

            $(self.options.tabContentItem).hide();

            selectedTab.show();

            var hashId = tabId.replace("-nav", "");
            window.location.hash = hashId;

            return false;
        };

        this.init = function () {
            $(self.options.tabMenuItem).click(self.tabClicked);

            var extraItems = self.options.extraItems; 
            if (typeof extraItems != "undefined" && extraItems != null) {
                for (var i = 0; i < extraItems.length; i++) 
                    $(extraItems[i]).click(self.tabClicked);
            }

            var openedTab = window.location.hash;
            openedTab += "-nav";
            var tabElement = $("a[href='" + openedTab + "']", $(self.options.tabs));

            if (tabElement.length > 0) {
                tabElement.click();
            }
            else if (typeof self.options.initial != "undefined") {
                var tab = $("a[href='" + self.options.initial + "']", $(self.options.tabs));
                tab.click();
            }
            else {
                var firstTab = $(self.options.tabs).first();
                var button = $("a", firstTab);
                button.click();
            }
        };

        this.init();
        return this;
    };
} (jQuery));
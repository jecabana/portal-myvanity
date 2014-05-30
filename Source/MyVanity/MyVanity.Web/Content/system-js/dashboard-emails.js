function DashboardMails(options, agentsModule) {
    var self = this;

    this.resetContainer = function(result, container) {
        $(container).empty();
        $(container).append(result);

        self.wireEvents(container);
    };

    this.performSearch = function (page, pageSize, isInbox) {
        var container = isInbox ? options.inboxContainer : options.outboxContainer;

        if (isInbox) {
            return $.ajax({
                url: "/Message/PartialInbox",
                data: { page: page, pageSize: pageSize },
                method: "GET",
                success: function (result) {
                    self.resetContainer(result, container);
                    $("div#collapse-inbox", options.inboxContainer).collapse("show");
                }
            });
        }

        return $.ajax({
                    url: "/Message/PartialSent",
                    data: { page: page, pageSize: pageSize },
                    method: "GET",
                    success: function (result) {
                        self.resetContainer(result, container);
                        $("div#collapse-outbox", options.outboxContainer).collapse("show");
                    }
        });
    };

    this.pagerBtnClicked = function() {
        var btn = $(this);

        var page = btn.attr("data-page");
        var pageSize = myvanity.defaultPageSize;
        var isInbox = btn.closest(options.inboxContainer).length !== 0;

        self.performSearch(page, pageSize, isInbox);
        return false;
    };

    this.wireEvents = function(container) {
        var pagerBtns = $("a[data-role='pager-button']", container);
        pagerBtns.click(self.pagerBtnClicked);

        if (container == options.inboxContainer) 
            agentsModule.WireMessageEvents();
    };

    this.init = function () {
        //go and fetch inbox messages
        self.performSearch(0, myvanity.defaultPageSize, true).done(function() {
            self.wireEvents(options.inboxContainer);
        });

        //go and fetch outbox messages
        self.performSearch(0, myvanity.defaultPageSize, false).done(function () {
            self.wireEvents(options.outboxContainer);
        });
    };

    return {
        Init: self.init
    };
}
function ComposeMailModule(options) {
    var self = this;

    self.lightbox = null;

    self.searchUsers = function() {
        var val = $("#" + options.inputTextId).val();

        $.ajax({
            url: "/User/GetToUsers/",
            method: "GET",
            data: { criteria: val, exceptCurrentUser: true },
            success: function(result) {
                self.lightbox = myvanity.showLightBox(result, {
                    hideAcceptButton: true
                });

                var body = self.lightbox.GetBody();
                self.wireEvents(body);
            }
        });
    };

    self.wireEvents = function(container) {
        var $selects = $("a.select-user", container);

        $selects.click(function() {
            var $item = $(this);
            var id = $item.attr("data-id");

            $("#user-id").val(id);
            $("#" + options.inputTextId).val($item.attr("data-name"));

            self.lightbox.Close();
        });
    };

    self.init = function () {
        var searchBtn = "#" + options.searchButtonId;

        $(searchBtn).click(self.searchUsers);
        $(searchBtn).tooltip();
    };

    return {
        Init: self.init
    };
}
function AppointmentsModule(options) {
    var self = this;

    self.lightbox = null;

    self.searchProcedures = function() {
        var val = $("#" + options.inputTextId).val();

        $.ajax({
            url: "/PatientProcedure/GetProcedures/",
            method: "GET",
            data: { name: val },
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
        var $selects = $("a.select-procedure", container);

        $selects.click(function() {
            var $item = $(this);
            var id = $item.attr("id");

            $("#procedure-id").val(id);
            $("#" + options.inputTextId).val($item.attr("data-name"));

            self.lightbox.Close();
        });
    };

    self.init = function() {
        $("#" + options.searchButtonId).click(self.searchProcedures);
    };

    return {
        Init: self.init
    };
}
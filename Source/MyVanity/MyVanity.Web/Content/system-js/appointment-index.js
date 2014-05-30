function AppointmentIndex(options) {
    var self = this;

    this.appointmentId = null;

    this.init = function() {
        $(options.buttonSel).click(self.showOptions);
    }

    this.showOptions = function() {
        self.appointmentId = $(this).attr("data-id");
        var currentStatus = $(options.statusSel + "-" + self.appointmentId).text();

        $.ajax({
            url: "/Appointments/GetStatuses",
            method: "GET",
            dataType: "json",
            success: function(result) {
                var list = result.statuses;
                var $select = $("<select />", { id: "statuses" });

                for (var i = 0; i < list.length; i++) {
                    if (currentStatus == list[i].name) continue;
                    $("<option />", { value: list[i].val, text: list[i].name }).appendTo($select);
                }

                myvanity.showLightBox($select[0], {
                    acceptCallback: self.saveStatus
                });
            }
        });
    };

    this.saveStatus = function() {
        var statusVal = $("#statuses option:selected").val();

        $.ajax({
            url: "/Appointments/SaveStatus",
            data: { newStatus: statusVal, id: self.appointmentId },
            method: "POST",
            success: function(result) {
                var newStat = $("#statuses option[value='" + result.newStatus + "']").text();
                $(options.statusSel + "-" + self.appointmentId).text(newStat);
            }
        });
    }

    return {
        Init: self.init
    }
}
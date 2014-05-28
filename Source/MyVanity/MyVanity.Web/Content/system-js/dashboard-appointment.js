function DashboardAppointmentsModule(options) {
    var self = this;

    this.changeStatus = function(status, appointmentId) {
        $.ajax({
            url: "/PatientDashboard/ChangeAppointmentStatus",
            method: "POST",
            data: {
                newStatus: status,
                appointmentId: appointmentId
            },
            success: function (result) {
                $(options.statusField + "-" + appointmentId).text(result.status);

                myvanity.decreaseBadge("#appointments-badge");
                $("#" + options.confirmBtn + "-" + appointmentId).hide();
                $("#" + options.cancelBtn + "-" + appointmentId).hide();
            }
        });
    };

    //See AppointmentStatus.cs
    this.confirmAppointment = function () {
        var id = $(this).attr("data-appointment-id");
        self.changeStatus(3, id, $(this));
    };

    this.cancelAppointment = function () {
        var id = $(this).attr("data-appointment-id");
        self.changeStatus(2, id, $(this));
    };

    this.init = function() {
        var confirmBtn = $("." + options.confirmBtn);
        var cancelBtn = $("." + options.cancelBtn);
        
        confirmBtn.click(self.confirmAppointment);
        cancelBtn.click(self.cancelAppointment);
    };

    return {
        Init: self.init
    };
}
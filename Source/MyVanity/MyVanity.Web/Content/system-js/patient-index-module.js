function PatientIndexModule(options) {
    var self = this;

    this.currentAgentId = "";
    this.currentPatientId = "";
    this.lightbox = null;

    this.showLightBox = function() {
        $.ajax({
            url: "/Patient/GetAgentsPartialView",
            data: {
                distinctFrom: self.currentAgentId
            },
            success: function(result) {
                self.lightbox = myvanity.showLightBox(result, {
                    acceptCallback: self.reassignCallback
                });
            }
        });
    };

    this.reassignCallback = function () {
        var container = self.lightbox.GetBody();
        var selected = $("#agents-dropdown option:selected", container).val();

        if (typeof selected !== "undefined" && selected != self.currentAgentId) {
            $.ajax({
                url: "/Patient/ReassignToAgent/",
                method: "POST",
                data: {
                    patientId: self.currentPatientId,
                    agentId: selected
                },
                success: function(result) {
                    if (result.success) {
                        window.location.reload();
                    } else {
                        myvanity.alertError("There was an error while processing your request, please trye again in a few seconds...");
                    }
                }
            });
        }
    };

    this.init = function() {
        var $btn = $(options.assignBtn);

        self.currentAgentId = $btn.attr("data-agent-id");
        self.currentPatientId = $btn.attr("data-patient-id");

        $btn.click(self.showLightBox);
    };

    return {
        Init: self.init
    };
}
function ConsentModule(options) {
    var self = this;

    this.toggleSpan = function() {
        var toggleElement = $(this).attr("data-consent-toggle");

        var sel = "a[href=#" + toggleElement + "]" + " span";
        var ticked = $(sel).hasClass("glyphicon-ok");

        $(sel).removeClass("glyphicon-remove glyphicon-ok");

        if (ticked)
            $(sel).addClass("glyphicon-remove");
        else
            $(sel).addClass("glyphicon-ok");
    };

    this.hideSaveBtn = function() {
        var chkboxes = $(options.checkboxes);

        if (chkboxes.length == 0)
            $(options.saveBtn).hide();
    };

    this.save = function() {
        var checked = $(options.checkboxes + ":checked");

        if (checked.length == 0) return;

        var consentIds = [];
        $.each(checked, function (i, item) {
            var id = $(this).attr("data-consent-id");
            consentIds.push(parseInt(id));
        });

        var procedureId = $("#procedure-id").val();
        var postData = {};

        postData["procedureId"] = procedureId;
        postData["consentIds"] = consentIds;
        postData = JSON.stringify(postData);

        $.ajax({
            url: "/PatientDashboard/SignConsent",
            data: postData,
            method: "POST",
            contentType: "application/json",
            success: function(result) {
                if (result.success) {
                    myvanity.decreaseBadge("#consent-badge");

                    $.each(consentIds, function(i, item) {
                        var chk = $("input[data-consent-id='" + item + "']");
                        //remove agreement checkbox
                        chk.parent().remove();
                        //hide saveBtn if no unsigned remaining
                        self.hideSaveBtn();
                    });
                }
            }
        });
    };

    this.init = function () {
        self.hideSaveBtn();
        $(options.checkboxes).click(self.toggleSpan);
        $(options.saveBtn).click(self.save);
    };

    return {
        Init: self.init
    };
}
function PatientDocumentsModule(options) {
    var self = this;

    this.lightbox = null;

    this.showLightBox = function () {
        $.ajax({
            url: "/Common/GetPartialViewForAddingFile",
            success: function(result) {
                self.lightbox = myvanity.showLightBox(result, {
                    hideAcceptButton: true
            });

                self.wireEvents(self.lightbox.GetBody());
            }
        });
    };

    this.wireEvents = function(container) {
        var browseBtn = $(options.browseBtn, container);

        browseBtn.fileupload({
            add: function(e, data) {
                var errors = myvanity.validateInputFile(data);

                if (errors.length == 0) {
                    $(options.errorField).text("");

                    var fileName = data.files[0].name;
                    $(options.fileNameField).text(fileName);

                    var $uploadBtn = $(options.uploadBtn, container);
                    $uploadBtn.show();
                    $uploadBtn.click(function () {
                        var body = self.lightbox.GetBody();
                        var description = $(options.fileDescription, body);

                        if (description[0].textLength == 0) {
                            self.addError("Please, add a description for the document");
                            return false;
                        }

                        $(this).button("loading");

                        var currentProcedureId = $(options.procedureId).val();

                        data.formData = { userProcedureId: currentProcedureId, description: description.val() };
                        data.submit();
                    });
                } else {
                    self.addError(errors.join("\n"));
                }
            },
            done: function(e, data) {
                var result = data.result;
                $(options.fileGuid).val(result.guid);
                self.lightbox.Close();
            },
            always: function() {
                $(options.uploadBtn, container).button("reset");
            }
        });
    };

    this.addError = function(error) {
        $(options.errorField).text(error);
    };

    this.init = function() {
        var $addFileBtn = $(options.addFileBtn);
        $addFileBtn.click(self.showLightBox);
    };

    return {
        Init: self.init
    };
}
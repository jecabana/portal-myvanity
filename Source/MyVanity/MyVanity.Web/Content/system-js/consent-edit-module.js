function ConsentEditModule(options) {
    var self = this;

    this.init = function () {
        var $browseBtn = $(options.browseBtn);
        var $importBtn = $(options.importBtn);

        $browseBtn.fileupload({
            add: function (e, data) {
                //clean previous validation errors
                $(options.errorField).text("");

                var uploadErrors = myvanity.validateInputFile(data, /(\.|\/)(doc|docx)$/i);
                if (uploadErrors.length !== 0) {
                    $(options.errorField).text(uploadErrors.join("\n"));
                    return;
                }

                $importBtn.show();
                $importBtn.click(function () {
                    $importBtn.button("loading");
                    data.submit();
                });

                var $filename = $(options.fileName);
                $filename.text(data.files[0].name);
            },
            done: function (e, data) {
                var result = data.result;

                if (result.success) {
                    $(options.filePath).val(result.filePath);
                    $(options.hiddenForm).show();

                    var body = result.body;
                    $(options.consentDescription).val(body);
                    $("#consent-container").html(body);
                } else {
                    myvanity.alertError(result.message, 5000);
                }
            },
            always: function() {
                $importBtn.button("reset");
            }
        });
    };

    return {
        Init: self.init
    };
}
function ImageUploader(options) {
    var self = this;

    this.init = function() {
        var ctx = this;
        this.textSel = ".fileinput-button > span";


        var fileUpload = $(options.inputSel).fileupload({
            // Enable image resizing, except for Android and Opera,
            // which actually support image resizing, but fail to
            // send Blob objects via XHR requests:
            disableImageResize: /Android(?!.*Chrome)|Opera/
                .test(window.navigator && navigator.userAgent),
            imageMaxWidth: options.width ? options.width : 500,
            imageMaxHeight: options.height ? options.height : 500,
            imageCrop: true, // Force cropped images
            acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
            maxFileSize: 4000000,
            add: function(e, data) {
                var that = this;

                data.formData = [{ name: "relPath", value: options.relPath }];
                $.blueimp.fileupload.prototype.options.add.call(that, e, data);
            },
            submit: function(e, data) {
                $(options.errorSpanSel).text("");

                ctx.prevText = $(ctx.textSel).text();
                $(ctx.textSel).text($(ctx.textSel).attr("data-loading-text"));
                $(options.inputSel).prop("disabled", true);
            },
            done: function(e, data) {
                var result = data.result;
                myvanity.alert(result.message, result.success);

                if (result.success)
                    $(options.inputGuidSel).val(result.guid);
            },
            always: function(e, data) {
                $(options.inputSel).prop("disabled", false);
                $(ctx.textSel).text(ctx.prevText ? ctx.prevText : "Browse");
            }
        }).on("fileuploadprocessalways", function(e, data) {
            var file = data.files[data.index];

            if (file.preview)
                $(options.imgTagSel).attr("src", window.URL.createObjectURL(file));
            if (file.error)
                $(options.errorSpanSel).text(file.error);
        });

        return fileUpload;
    };

    return {
        Init : self.init
    };
}
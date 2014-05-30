function FileUploadModule(options) {
    var self = this;

    var defaults = {
        uploadUrl: "/FileManaging/UploadFile",
        filebtnSel: "#fileupload",
        uploadbtnSel: "#btnupload",
        modelPopertyName: "Files"
    };

    this.viewModel = function() {
        this.addedFiles = ko.observableArray();
        this.currentFile = ko.observable();
    };

    this.viewModel.prototype.removeFile = function(file) {
        self.viewModel.addedFiles.remove(file);
    };

    this.file = function(id, name, description) {
        this.id = id;
        this.name = name;
        this.description = description;
    };

    this.options = $.extend({}, defaults, options);

    this.init = function () {
        var sel = self.options.filebtnSel;

        ko.applyBindings(self.viewModel);

        $(sel).fileupload({
            add: function (e, data) {
                var btnSel = self.options.uploadbtnSel;
                $(btnSel).click(function () {
                    data.submit();
                });
            }
        });
    };
}
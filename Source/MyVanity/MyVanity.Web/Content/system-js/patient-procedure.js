function UserProcedureViewModel() {
    var vm = this;

    //doctors management
    this.addedDoctors = ko.observableArray();

    this.addExistingDoctor = function (doctor) {
        vm.addedDoctors.push(doctor);
    };

    this.currentDoctor = function () {
        return {
            id: $("#doctor-select option:selected").val(),
            name: $("#doctor-select option:selected").text()
        };
    };

    this.addDoctor = function () {
        var doctorToAdd = vm.currentDoctor();

        if (doctorToAdd.id == "")
            return;

        for (var i = 0; i < vm.addedDoctors().length; i++) {
            if (doctorToAdd.id == vm.addedDoctors()[i].id)
                return;
        }

        vm.addExistingDoctor(vm.currentDoctor());
    };

    this.removeDoctor = function (item) {
        vm.addedDoctors.remove(item);
    };

    //files management
    this.addedFiles = ko.observableArray();

    this.addFile = function (file) {
        var files = vm.addedFiles();

        for (var i = 0; i < files.length; i++) {
            if (file.id == files[i].id)
                return;
        }

        vm.addedFiles.push(file);
    };

    this.removeFile = function(file) {
        vm.addedFiles.remove(file);
    };

    //consents management
    this.consent = function(id, consentId, title, signed) {
        this.id = id;
        this.consentId = consentId;
        this.title = title;
        this.signed = signed;
    };

    this.addedConsents = ko.observableArray();

    this.currentConsent = function() {
        return {
            id: $("#consent-select option:selected").val(),
            name: $("#consent-select option:selected").text()
        };
    };

    this.addExistingConsent = function (consent) {
        var consents = vm.addedConsents();

        for (var i = 0; i < consents.length; i++) {
            if (consent.consentId == consents[i].consentId)
                return;
        }

        vm.addedConsents.push(consent);
    };

    this.addNewConsent = function() {
        var consentToAdd = vm.currentConsent();

        if (consentToAdd.id == "")
            return;

        consentToAdd = new vm.consent(0, consentToAdd.id, consentToAdd.name, 'false');
        vm.addExistingConsent(consentToAdd);
    };

    this.removeConsent = function(consent) {
        vm.addedConsents.remove(consent);
    };
}

function UserProcedureModule(options) {
    var self = this;

    self.lightbox = null;

    self.searchPatients = function() {
        var val = $("#" + options.inputTextId).val();

        $.ajax({
            url: "/Patient/GetPatients/",
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

    self.searchSharedFiles = function () {
        var val = $("#" + options.fileNameId).val();

        $.ajax({
            url: "/Resources/GetFilesBy",
            method: "GET",
            data: { criteria: val },
            success: function (result) {
                self.lightbox = myvanity.showLightBox(result, {
                    acceptButtonTitle: "Add",
                    acceptCallback: self.addFilesCallback
                });
            }
        });

    };

    self.addFilesCallback = function() {
        var body = self.lightbox.GetBody();

        var selected = $("#file-selected:checked", body);
        $.each(selected, function (i, item) {
            var file = $(item);

            var id = file.attr("data-id");
            var name = file.attr("data-name");
            var path = file.attr("data-path");

            self.viewModel.addFile({ id: id, name: name, path: path });
        });
    };

    self.wireEvents = function(container) {
        var $selects = $("a.select-user", container);

        $selects.click(function() {
            var $item = $(this);
            var id = $item.attr("data-id");

            $("#patient-id").val(id);
            $("#" + options.inputTextId).val($item.attr("data-name"));

            self.lightbox.Close();
        });
    };

    self.viewModel = new UserProcedureViewModel();

    self.init = function () {
        var searchBtn = "#" + options.searchButtonId;
        $(searchBtn).click(self.searchPatients);
        $(searchBtn).tooltip();

        var searchFileBtn = "#" + options.searchFileButtonId;
        $(searchFileBtn).click(self.searchSharedFiles);
        $(searchFileBtn).tooltip();
    };

    return {
        Init: self.init,
        ViewModel: self.viewModel
    };
}
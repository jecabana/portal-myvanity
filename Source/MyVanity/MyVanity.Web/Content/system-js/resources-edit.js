function ResourcesViewModel() {
    var vm = this;

    this.subcategories = ko.observableArray();
    this.currentSubcat = ko.observable();

    this.selectedSubcatId = ko.computed(function() {
        if (vm.currentSubcat()) return vm.currentSubcat().id;

        return null;
    });
}

function ResourcesEdit(options) {
    var self = this;

    self.viewModel = new ResourcesViewModel();

    ResourcesViewModel.prototype.addSubcat = function(subcat) {
        self.viewModel.subcategories.push(subcat);
    };

    this.category = function (id, name) {
        this.id = id;
        this.name = name;
    };

    this.categoryChanged = function () {
        var catId = $(options.categoriesDd + " option:selected").val();

        if (catId == "") {
            self.viewModel.subcategories.removeAll();
            self.viewModel.currentSubcat(null);
            return false;
        }

        $.ajax({
            url: "/Resources/GetSubcatsForCat",
            method: "POST",
            data: { catId: catId },
            success: function(result) {
                if (typeof result.subcats !== "undefined") {
                    self.viewModel.subcategories.removeAll();

                    $.each(result.subcats, function (i, cat) {
                        var subCat = new self.category(cat.Id, cat.Name);
                        self.viewModel.addSubcat(subCat);
                    });
                }
            }
        });
    };

    this.setFileName = function (val) {
        val = val.replace(/.*(\/|\\)/, '');
        $(options.fileName).text(val);
        $(options.realName).val(val);
    };

    this.uploadChanged = function() {
        var newVal = $(options.fileInput).val();
        self.setFileName(newVal);
    };

    this.init = function () {
        ko.applyBindings(self.viewModel);
        $(options.categoriesDd).change(self.categoryChanged);
        $(options.fileInput).change(self.uploadChanged);

        self.categoryChanged();
    };

    return {
        Init: self.init,
        SetFileName: self.setFileName
    };
}
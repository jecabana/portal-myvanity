var myvanity = window.myvanity || {};

myvanity.LightBox = null;

myvanity.showLightBox = function (body, options) {
    if (typeof (myvanity.LightBox) == "undefined" || myvanity.LightBox == null) {
        myvanity.LightBox = new LightBox(options);
    } else {
        myvanity.LightBox.SetOptions(options);
    }

    myvanity.LightBox.Show(body);
    return myvanity.LightBox;
};

myvanity.init = function() {
    $(".datetime").datepicker();
};

myvanity.getFileExt = function(file) {
    return file.split(".").pop();
};

myvanity.removeFileExt = function(filename) {
    var splitted = filename.split(".");
    splitted.pop();
    return splitted.join();
};

myvanity.alertSuccess = function(text) {
    var $container = $("<div>").addClass("alert-container");
    $("<div>").addClass("alert alert-success")
        .text(text)
        .appendTo($container);

    return $container.appendTo(document.body)
        .hide(2000);
};

myvanity.alertError = function(text, duration) {
    var $container = $("<div>").addClass("alert-container");
    $("<div>").addClass("alert alert-danger")
        .text(text)
        .appendTo($container);

    var ms = duration ? duration : 2000;
    return $container.appendTo(document.body)
        .hide(ms);
};

myvanity.alert = function(text, success) {
    return success ? myvanity.alertSuccess(text) : myvanity.alertError(text);
};

myvanity.decreaseBadge = function(badgeSel) {
    var badge = $(badgeSel);
    var amount = parseInt(badge.text());

    if (typeof amount === "number") {
        amount = amount - 1;

        if (amount === 0)
            badge.remove();
        else
            badge.text(amount);
    }
};

myvanity.validateInputFile = function(data, regex, fileSize) {
    var uploadErrors = [];

    var acceptFileTypes = /(\.|\/)(jpe?g|png|doc|docx|pdf|mp4|ogg)$/i;
    if (regex) acceptFileTypes = regex;

    var file = data.originalFiles[0];
    if (!(acceptFileTypes.test(file.type) || acceptFileTypes.test(file.name))) {
        uploadErrors.push('Not an accepted file type');
    }

    var size = 5000000;
    if (fileSize) size = fileSize;

    if (data.originalFiles[0]['size'].length && data.originalFiles[0]['size'] > size) {
        uploadErrors.push('Filesize is too big');
    }

    return uploadErrors;
};

myvanity.defaultPageSize = 10;

$(function () {
    myvanity.init();
})
$.fn.lightboxCount = 0;

function LightBox(options) {
    var self = this;

    this.container = null;

    this.defaults = {
        nodeId: "#lightbox",
        overlayId: "#overlay",
        containerId: "div.lightbox-container",
        closedCallback: null,
        acceptCallback: null,
        hideAcceptButton: false,
        hideCloseButton: false,
        acceptButtonTitle: "Accept",
        closeButtonTitle: "Close"
    };

    var pluginOptions = $.extend({}, this.defaults, options);
    
    this.options = pluginOptions;

    this.setOptions = function (newOptions) {
        var opt = $.extend({}, self.defaults, newOptions);
        self.options = opt;
    };

    this.init = function () {
        
    };

    this.getLightboxContainerNode = function () {
        if (self.container == null) {
            var source = $(self.options.nodeId);
            var clone = source.clone()[0];
            
            $(document.body).append(clone);
            
            self.container = $(clone);
            $.fn.lightboxCount++;
            var newId = "lightbox-" + $.fn.lightboxCount;
            self.container.attr("id", newId);
        }

        return self.container;
    };

    this.show = function (content) {
        var lightbox = self.getLightboxContainerNode();
        var contentNode = $(content);
        var overlay = $(self.options.overlayId);

        if (lightbox.length == 0)
            return false;
        
        var container = $(self.options.containerId, lightbox);
        container.html(contentNode);

        self.configureDialog();
        self.getLightboxContainerNode().css("display", "block");
        
        overlay.fadeIn();

        return true;
    };

    this.configureDialog = function () {
        var closeButton = $("a.close-popup");
        var closeButtonItem = $("a.cancel-popup");
        
        var acceptButton = $("a.accept-popup");
        var overlay = $(self.options.overlayId);

        closeButton.click(self.close);
        overlay.click(self.close);

        acceptButton.off('click');
        acceptButton.click(self.acceptButtonClicked);

        if (self.options.hideAcceptButton)
            acceptButton.hide();
        else
            acceptButton.show();

        if (self.options.hideCloseButton) {
            closeButton.hide();
        } else {
            closeButton.show();
        }

        acceptButton.text(self.options.acceptButtonTitle);
        closeButtonItem.text(self.options.closeButtonTitle);
    };

    this.acceptButtonClicked = function () {
        var acceptCtx = this;
        if (typeof self.options.acceptCallback === "function") {
            var result = self.options.acceptCallback(self);
            if (result === false) return false;
        }

        return self.close(acceptCtx);
    };

    this.getContainerNode = function () {
        return self.container;
    };

    this.close = function (acceptCtx) {
        var overlay = $(self.options.overlayId);
        var lightbox = self.getLightboxContainerNode();

        lightbox.css("display", "none");
        overlay.fadeOut();

        if (typeof self.options.closedCallback === "function")
            self.options.closedCallback(self);

        $(acceptCtx).off('click');
        
        return true;
    };

    return {
        Init: self.init,
        Show: self.show,
        Close: self.close,
        GetBody: self.getContainerNode,
        SetOptions: self.setOptions
    };
}


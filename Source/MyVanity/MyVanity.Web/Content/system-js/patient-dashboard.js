function DashboardModule() {
    var self = this;

    this.modules = [];

    this.supportsVideoPlayback = function() {
        return Modernizr.video;
    };

    this.addModule = function(module) {
        self.modules.push(module);
    };

    this.init = function() {
        if (!self.supportsVideoPlayback) {
            var listWrapper = $("<ul class='articles-list list-unstyled'>");
            var articles = $(".video-small-desc");

            $.each(articles, function (i, item) {
                $(item).show();

                listWrapper.append($("<li>")
                           .append(item));
            });


            $(".video-widget").hide();
            $(".video-list").append(listWrapper[0]);
        }

        $.each(self.modules, function(i, mod) {
            mod.Init();
        });
    };

    return {
        Init: self.init,
        AddModule: self.addModule
    };
}
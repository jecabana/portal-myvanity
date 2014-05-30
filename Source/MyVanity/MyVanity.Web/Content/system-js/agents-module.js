function AgentsModule(rootContainerSel, options) {
    var self = this;

    this.sendBtn = $("#send-btn", rootContainerSel);
    this.currentAgentId = "";
    this.repliesToId = null;

    this.sendMessage = function () {
        var body = $("#message-body", rootContainerSel);
        if (body.val() == "")
            return;

        var toId = self.currentAgentId, 
            fromId = $("#current-patient-id").val();
        var btn = $(this);
        btn.button("loading");

        $.ajax({
            url: "/Message/MessageTo",
            method: "POST",
            data: {
                toId: toId,
                fromId: fromId,
                body: body.val(),
                repliesTo: self.repliesToId
            },
            success: function (result) {
                if (result.success) {
                    $(options.successMessage).fadeIn();
                    $(options.successMessage).fadeOut(5000);
                } else {
                    myvanity.alertError("There was an error sending your message, please try again in a few seconds", 5000);
                }
            }
        }).always(function() {
            btn.button("reset");
            $(body).val("");

            //reset reply
            self.repliesToId = null;
        });
    };

    this.setCurrentAgent = function(id, name) {
        self.currentAgentId = id;
        $(options.btnHeader).text(name);
    };

    this.reply = function () {
        var toId = $(this).attr("data-reply-to");
        self.repliesToId = $(this).attr("data-message-id");

        var currentAgent = $("a[data-agent-id='" + toId + "']");
        var name = currentAgent.text();
        
        self.setCurrentAgent(toId, name);
        $("#message-body").focus();
    };

    this.setMessageRead = function () {
        var collapse = $(this);
        var labelNew = collapse.parent().find("span.label");

        if (labelNew.length === 0) 
            return true;

        var id = collapse.attr("data-message-id");
        
        $.ajax({
            url: "/Message/SetMessageRead",
            data: { messageId: id },
            method: "POST",
            success: function (result) {
                if (result.success) {
                    labelNew.remove();
                    myvanity.decreaseBadge(options.badgeSel);
                }
            }
        });
    };

    this.wireEvents = function () {
        self.sendBtn.click(self.sendMessage);
        self.wireMessageEvents();
        $(options.agentBtns).click(function () {
            var agent = $(this);
            self.setCurrentAgent(agent.attr("data-agent-id"), agent.text());
        });
        
    };

    this.wireMessageEvents = function() {
        $(options.replyBtn).click(self.reply);
        $(options.collapseBtns).click(self.setMessageRead);
    };

    this.init = function () {
        self.wireEvents();
    };

    return {
        Init: self.init,
        SetAgent: self.setCurrentAgent,
        WireMessageEvents: self.wireMessageEvents
    };
}
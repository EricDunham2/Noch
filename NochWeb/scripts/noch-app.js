/* Knockout View Model */
function chatViewModel() {
    _this = this;
    this.messages = ko.observableArray();
    this.domains = ko.observableArray();
    this.newChannelName = ko.observable();
    this.selectedDomain = ko.observable({
        DomainID: ko.observable(),
        Name: ko.observable(),
        Channels: ko.observableArray()
    });
    this.channelModal = function(data, event) {
        _this.selectedDomain()
            .DomainID(data.DomainID)
            .Name(data.Name)
            .Channels(data.Channels);
        $('#channelModal').modal('show');
    };
    this.makeChannel = function (data, event) {
        var domainId = $(event.currentTarget).data('domainid');
        makeChannel(_this.newChannelName(), domainId);
    };
    this.getMessages = function (data, event) {      
        getChannelMessages(data.ChannelID);
        $('#channelModal').modal('hide');
    };
    this.channelCollapse = function(data, event) {
        var name = $(event.currentTarget).data('domain-name');
        $('#' + name + '_domain').collapse('show');
    };
    this.showEditBox = function (data, event) {
        var msgIdx = _this.messages.indexOf(data);
        if (msgIdx !== -1) {
            _this.messages()[msgIdx].isEditable(!_this.messages()[msgIdx].isEditable());
        }
    };
    this.editMessage = function (data, event) {
        var chat = $.connection.chatHub;
        chat.server.update(data.id, data.message);

        $.ajax({
            url: '/Chat/UpdateMessage',
            type: 'POST',
            data: JSON.stringify({msgId: data.msgId, msg: data.message }),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function () {

            },
            error: function (request, status, error) {
                console.log(error);
            }
        });
    };
}

/* Main View-Model */
var vm = {};

/* Object for accessing state info (it's passed through the script tag) */
var VARS = VARS || (function () {
    var _args = {}; // private

    return {
        init: function (Args) {
            _args = Args;
        },
        getUserName: function () {
            return _args.username;
        },
        getUserID: function () {
            return _args.userid;
        },
        getChannelID: function () {
            return _args.channelid;
        },
        setChannelID: function (channelId) {
            _args.channelid = channelId;
        }
    };
} ());

$(document).ready(function () {

    vm = new chatViewModel();
    getDomainsForCurrentUser().done(function () {
        ko.applyBindings(vm);
    });
    // Declare a proxy to reference the hub.
    var chat = $.connection.chatHub;
    // Create a function that the hub can call to broadcast messages.
    chat.client.broadcastMessage = function (name, message) {

        /* push back messagees to observable array, which automatically updates the discussion ul
		   you can see on the index page the 'data-bind' attribute that binds to this array */
        vm.messages.push({
            name: name,
            message: message
        });
    };

    // Set initial focus to message input box.
    $('#message').focus();
    // Start the connection.
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            // get some chat variables 
            var msg = $('#message').val();
            // Call the Send method on the hub.
            var a = VARS;
            chat.server.send(VARS.getUserName(), msg);

            saveMessage({
                userId: VARS.getUserID(),
                channelId: VARS.getChannelID(),
                msg: msg
            });

            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();
        });
        $('#message').keyup(function (e) {
            var inputVal = $('#message').val();
            if (!!inputVal) {
                var charCode = (typeof e.which == "number") ? e.which : e.keyCode;
                if (charCode == 13) { // Looks for enter key press
                    e.stopPropagation();
                    // get some chat variables 
                    var msg = $('#message').val();
                    // Call the Send method on the hub.
                    var a = VARS;
                    chat.server.send(VARS.getUserName(), msg);

                    saveMessage({
                        userId: VARS.getUserID(),
                        channelId: VARS.getChannelID(),
                        msg: msg
                    });

                    // Clear text box and reset focus for next comment.
                    $('#message').val('').focus();
                    $('#message').val(""); // Clears the text box
                    return false;
                }
            }
        })
    });
});

function getChannelMessages(channelId) {
    return $.ajax({
        url: '/Chat/GetMessages',
        type: 'GET',
        data: {
            channelId: channelId,
            messageCount: 50
        },
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (messages) {
            vm.messages.removeAll();
            for (var i = 0; i < messages.length; ++i) {

                var msg = { name: ko.observable(messages[i].Username), message: ko.observable(messages[i].Content), id: ko.observable(messages[i].MessageID), isEditable: ko.observable(false) };

                vm.messages.push(msg);
            }
            VARS.setChannelID(channelId);
        },
        error: function (data) {
        }
    });
};

function getDomainsForCurrentUser() {
    return $.ajax({
        url: '/Chat/GetDomainsForCurrentUser',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (null !== data) {
                vm.domains.removeAll();
                for (var idx = 0; idx < data.length; ++idx) {
                    vm.domains.push(data[idx]);
                }
            }
        },
        error: function (data) {
        }
    });
};

function makeDomain(name) {
    return $.ajax({
        url: '/Chat/MakeDomain/',
        type: 'POST',
        data: JSON.stringify({ newDomain: name, userid: VARS.getUserID() }),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function () {
            getDomainsForCurrentUser();
        },
        error: function (request, status, error) {
        }
    });
}


function updateMessage(msgUserId, msgId, msg) {


    if (msgUserId == VARS.getUserID())
        return $.ajax({
            url: '/Chat/UpdateMessage/',
            type: 'POST',
            data: JSON.stringify({ newDomain: name, userid: VARS.getUserID() }),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function () {
            },
            error: function (request, status, error) {
            }
        });
}


function makeChannel(nameIn, domainid) {
    return $.ajax({
        url: '/Chat/MakeChannel/',
        type: 'POST',
        data: JSON.stringify({ name: nameIn, did: domainid }),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function () {
            getDomainsForCurrentUser();
        },
        error: function (request, status, error) {
        }
    });
}

function saveMessage(msgInfo) {
    return $.ajax({
        url: '/Chat/SendMessage/',
        type: 'POST',
        data: JSON.stringify(msgInfo),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8'
    });
}
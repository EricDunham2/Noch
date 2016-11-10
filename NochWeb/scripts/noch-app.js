/* Knockout View Model */
var chatViewModel = {
    messages: ko.observableArray()
};

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
}());

$(document).ready(function () {

    ko.applyBindings(chatViewModel);

    // Declare a proxy to reference the hub.
    var chat = $.connection.chatHub;
    // Create a function that the hub can call to broadcast messages.
    chat.client.broadcastMessage = function (name, message) {

        /* push back messagees to observable array, which automatically updates the discussion ul
		   you can see on the index page the 'data-bind' attribute that binds to this array */
        chatViewModel.messages.push({
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
            console.log(messages);
            chatViewModel.messages.removeAll();
            for (var i = 0; i < messages.length; ++i) {

                chatViewModel.messages.push({ name: messages[i].Username, message: messages[i].Content });
            }
            VARS.setChannelID(channelId);
        },
        error: function (data) {
            console.log(data);
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
        },
        error: function (request, status, error) {
            console.log(error)
        }
    });
}

function makeChannel(nameIn, domainid) {
    return $.ajax({
        url: '/Chat/MakeChannel/',
        type: 'POST',
        data: JSON.stringify({ name: nameIn, did:domainid }),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function () {
        },
        error: function (request, status, error) {
            console.log(error)
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
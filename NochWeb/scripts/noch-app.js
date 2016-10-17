var chatViewModel = {
	messages: ko.observableArray()
};

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

		// push back messagees to observable array, which automatically updates the discussion ul
		// you can see on the index page the 'data-bind' attribute that binds to this array
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

			//call the controller method to send the message to the database
			$.ajax({
				url: '/Chat/SendMessage/',
				type: 'POST',
				data: JSON.stringify({
					userId: VARS.getUserID(),
					channelId: VARS.getChannelID(),
					msg: msg
				}),
				dataType: 'json',
				contentType: 'application/json; charset=utf-8'
			});

			// Clear text box and reset focus for next comment.
			$('#message').val('').focus();
		});
	});
});

function getChannelMessages(channelId) {
    $('#discussion').empty();

    $.ajax({
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
            for (var i = 0; i < messages.length; ++i) {
                $('#discussion').append('<li><strong>' + messages[i].Username
				+ '</strong>:&nbsp;&nbsp;' + messages[i].Content + '</li>');
            }
            VARS.setChannelID(channelId);
        },
        error: function (data) {
            console.log(data);
        }
    });
}
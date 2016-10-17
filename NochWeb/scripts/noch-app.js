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
				channelId: 1,
				msg: msg
			})

			// Clear text box and reset focus for next comment.
			$('#message').val('').focus();
		});
	});

	//send the message to the database
});

function saveMessage(msgInfo) {
	return $.ajax({
		url: '/Chat/SendMessage/',
		type: 'POST',
		data: JSON.stringify(msgInfo),
		dataType: 'json',
		contentType: 'application/json; charset=utf-8'
	});
}
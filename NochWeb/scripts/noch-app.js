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

	// Get the user name and store it to prepend to messages.
	$('#displayname').val(prompt('Enter your name:', ''));
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
					channelId: 1,
					msg: msg
				}),
				dataType: 'json',
				contentType: 'application/json; charset=utf-8'
			});

			// Clear text box and reset focus for next comment.
			$('#message').val('').focus();
		});
	});

	//send the message to the database
});
function getChannelMessages(channelId) {
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
        },
        error: function (data) {
            console.log(data);
        }
    });
}
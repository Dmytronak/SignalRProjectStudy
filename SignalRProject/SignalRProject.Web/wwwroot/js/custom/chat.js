'use strict';
var currentRoomId;
let token = localStorage.getItem('access_token');
const connection = new signalR.HubConnectionBuilder()
    .withUrl('/chatHub', { accessTokenFactory: () => token })
    .configureLogging({
        log: function (logLevel, message) {
            console.log(new Date().toISOString() + ': ' + message);
        }
    })
    .build();

document.getElementById('sendButton').disabled = true;
document.getElementById('chatBody').hidden = true;

connection.on('UserConnected', function (message,data, usersInRoom) {
    let notifyElem = document.createElement('b');
    notifyElem.appendChild(document.createTextNode(message));
    let elem = document.createElement('div');
    elem.className = 'notification-item-add'; 
    elem.appendChild(notifyElem);

    document.getElementById('messagesList').appendChild(elem);
    document.getElementById('userList').innerHTML = '';

    const usersDivCurrent = document.createElement('ul');
    usersDivCurrent.className = 'user-list';

    let userHeader = document.createElement('p');
    userHeader.className = 'user-list-header';
    userHeader.appendChild(document.createTextNode('Users in room:'));

    document.getElementById('userList').appendChild(userHeader);

    usersDivCurrent.innerHTML = `
            <li class='user-list-item' id='userListItem${data.id}'>
            <div class='nick-name small'>
                <img class='avatar' src='/${data.photo}'>
                ${data.firstName} ${data.lastName}
            </div>
            </li>
            `;

    for (var i = 0; i < usersInRoom.users.length; i++) {
        if (usersInRoom.users[i].id !== data.id) {
            usersDivCurrent.innerHTML += `
            <li class='user-list-item' id='userListItem${usersInRoom.users[i].id}'>
            <div class='nick-name small'>
                <img class='avatar' src='/${usersInRoom.users[i].photo}'>
                ${usersInRoom.users[i].firstName} ${usersInRoom.users[i].lastName}
            </div>
            </li>
            `;

        }
    }
    document.getElementById('userList').appendChild(usersDivCurrent);


});

connection.on('UserDisconnected', function (message, data) {
    let notifyElem = document.createElement('b');
    notifyElem.appendChild(document.createTextNode(message));
    let elem = document.createElement('div');
    elem.className = 'notification-item-remove'; 
    elem.appendChild(notifyElem);
    document.getElementById('messagesList').appendChild(elem);
    document.getElementById(`userListItem${data.id}`).remove();
});


connection.on('ReceiveMessage', function (data) {
    const leftSide = 'left';
    const rightSide = 'right';
    let side = '';

    const messegeDiv = document.createElement('div');
    messegeDiv.className = 'message-left';
    side = leftSide;
    if (data.userId == userId) {
        messegeDiv.className = 'message-right';
        side = rightSide;
    }
    messegeDiv.innerHTML = `
            <img class='avatar' src='/uploaded-images/users/icons/${data.userId}.png'>
            <div class='header'>
                <small class=' text-muted'><span class='fas fa-clock'></span>${data.creationAt}</small>
                <strong class='pull-${side} primary-font'>${data.fullName} </strong>
            </div>
            <p class='message-text'>${data.text}</p
            `;
    document.getElementById('messagesList').appendChild(messegeDiv);
    document.getElementById('messageInput').value = '';

    scrolToBottom();
});


connection.on('ReceiveRoomMessages', function (data) {
    const leftSide = 'left';
    const rightSide = 'right';
    let side = '';

    if (document.getElementById('messagesList').childElementCount < 2) {
        for (var i = 0; i < data.messages.length; i++) {
            const messegeDiv = document.createElement('div');
            messegeDiv.className = 'message-left';
            side = leftSide;
            if (data.messages[i].userId == userId) {
                messegeDiv.className = 'message-right';
                side = rightSide;
            }
            messegeDiv.innerHTML = `
            <img class='avatar' src='/uploaded-images/users/icons/${data.messages[i].userId}.png'>
            <div class='header'>
                <small class=' text-muted'><span class='fas fa-clock'></span>${data.messages[i].creationAt}</small>
                <strong class='pull-${side} primary-font'>${data.messages[i].fullName} </strong>
            </div>
            <p class='message-text'>${linkify(data.messages[i].text)}</p
            `;
            document.getElementById('messagesList').appendChild(messegeDiv);
            document.getElementById('messageInput').value = '';
        }
    }

    scrolToBottom();
   
});

connection.start().then(function () {
    document.getElementById('sendButton').disabled = false;
}).catch(function (err) {
    if (err.statusCode === 401) {
        logOut();
    }
    return console.error(err.toString());
});


function SendMassage() {
    var message = document.getElementById('messageInput').value;
    var roomName = $('#roomName').text();

    if (roomName) {
        connection.invoke('SendMessageToRoom', roomName, currentRoomId, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
}

function getRoomById(id, name) {
    const rommNameToLeft = $('#roomName').text();
    document.getElementById('chatBody').hidden = false;
    document.getElementById('messagesList').innerHTML = '';
    document.getElementById('userList').innerHTML = '';
    if (rommNameToLeft) {
        connection.invoke('LeaveRoom', rommNameToLeft).catch(function (err) {
            if (err.statusCode === 401) {
                logOut();
            }
            return console.error(err.toString());
        });
    }
    connection.invoke('JoinRoom', id, name).catch(function (err) {
        if (err.statusCode === 401) {
            logOut();
        }
        return console.error(err.toString());
    });

    $('#roomName').text(name);
    $('.jumbotron').hide();
    $('.chat-body').show();
    currentRoomId = id;
    scrolToBottom();

}
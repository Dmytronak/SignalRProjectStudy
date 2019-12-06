"use strict";
var currentRoomId;
let token = localStorage.getItem('access_token');
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub", { accessTokenFactory: () => token })
    .configureLogging({
        log: function (logLevel, message) {
            console.log(new Date().toISOString() + ": " + message);
        }
    })
    .build();

document.getElementById("sendButton").disabled = true;
document.getElementById("chatBody").hidden = true;

connection.on("UserConnected", function (message,data, usersInRoom) {
    let notifyElem = document.createElement("b");
    notifyElem.appendChild(document.createTextNode(message));
    let elem = document.createElement("p");
    elem.appendChild(notifyElem);

    document.getElementById("messagesList").appendChild(elem);
    document.getElementById("userList").innerHTML = '';

    const usersDivCurrent = document.createElement('ul');
    usersDivCurrent.className = 'user-list';
    usersDivCurrent.innerHTML = `
            <li id="userListItem${data.id}">
            <img class="avatar" src="/${data.photo}">
            <div class="nick-name">${data.firstName} ${data.lastName}</div>
            </li>
            `;

    for (var i = 0; i < usersInRoom.users.length; i++) {
        if (usersInRoom.users[i].id !== data.id) {

            usersDivCurrent.innerHTML += `
            <li id="userListItem${usersInRoom.users[i].id}">
            <img class="avatar" src="/${usersInRoom.users[i].photo}">
            <div class="nick-name">${usersInRoom.users[i].firstName} ${usersInRoom.users[i].lastName}</div>
            </li>
            `;

        }
    }
    document.getElementById("userList").appendChild(usersDivCurrent);


});

connection.on("UserDisconnected", function (message, data) {
    let notifyElem = document.createElement("b");
    notifyElem.appendChild(document.createTextNode(message));
    let elem = document.createElement("p");
    elem.appendChild(notifyElem);
    document.getElementById("messagesList").appendChild(elem);
    document.getElementById(`userListItem${data.id}`).remove();
});


connection.on("ReceiveMessage", function (data) {
    const messegeDiv = document.createElement('div');
    messegeDiv.className = 'message';
    messegeDiv.innerHTML = `
           <div class="nick-name">${data.fullName}</div>
            <img class="avatar" src="/uploaded-images/users/icons/${data.userId}.png">
            <div class="datetime">${data.creationAt}</div>
            <p class="message-text">${data.text}</p
            `;
    document.getElementById("messagesList").appendChild(messegeDiv);
    document.getElementById("messageInput").value = '';
});


connection.on("ReceiveRoomMessages", function (data) {

    if (document.getElementById("messagesList").childElementCount < 2) {
        for (var i = 0; i < data.messages.length; i++) {
            const messegeDiv = document.createElement('div');
            messegeDiv.className = 'message';
            messegeDiv.innerHTML = `
            <div class="nick-name">${data.messages[i].fullName}</div>
            <img class="avatar" src="/uploaded-images/users/icons/${data.messages[i].userId}.png">
            <div class="datetime">${data.messages[i].creationAt}</div>
            <p class="message-text">${data.messages[i].text}</p
            `;
            document.getElementById("messagesList").appendChild(messegeDiv);
            document.getElementById("messageInput").value = '';
        }
    }
   
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

window.setInterval(function () {
    var elem = document.getElementsByClassName('chat-container')[0];
    elem.scrollTop = elem.scrollHeight;
}, 5000);

function SendMassage() {
    var message = document.getElementById("messageInput").value;
    var roomName = $('#roomName').text();

    //if (!roomName) {
    //    connection.invoke("SendMessageToAll", message).catch(function (err) {
    //        return console.error(err.toString());
    //    });
    //}
    if (roomName) {
        connection.invoke("SendMessageToRoom", roomName, currentRoomId, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
}

function getRoomById(id, name) {
    const rommNameToLeft = $('#roomName').text();
    document.getElementById("chatBody").hidden = false;
    document.getElementById("messagesList").innerHTML = '';
    document.getElementById("userList").innerHTML = '';
    if (rommNameToLeft) {
        connection.invoke("LeaveRoom", rommNameToLeft).catch(function (err) {
            return console.error(err.toString());
        });
    }
    connection.invoke("JoinRoom", id, name).catch(function (err) {
        return console.error(err.toString());
    });

    $('#roomName').text(name);
    $('.jumbotron').hide();
    $('.chat-body').show();
    currentRoomId = id;

}
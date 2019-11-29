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

connection.on("UserConnected", function (message) {
    let notifyElem = document.createElement("b");
    notifyElem.appendChild(document.createTextNode(message));
    let elem = document.createElement("p");
    elem.appendChild(notifyElem);
    var firstElem = document.getElementById("messagesList").firstChild;
    document.getElementById("messagesList").insertBefore(elem, firstElem);
});

connection.on("UserDisconnected", function (message) {
    let notifyElem = document.createElement("b");
    notifyElem.appendChild(document.createTextNode(message));
    let elem = document.createElement("p");
    elem.appendChild(notifyElem);
    var firstElem = document.getElementById("messagesList").firstChild;
    document.getElementById("messagesList").insertBefore(elem, firstElem);
});

connection.on("ReceiveMessage", function (user, userId, message, dateTime) {

    const messegeDiv = document.createElement('div');
    messegeDiv.className = 'message';
    messegeDiv.innerHTML = `
            <div class="nick-name">${user}</div>
            <img class="avatar" src="/uploaded-images/users/icons/${userId}.png">
            <div class="datetime">${dateTime}</div>
            <p class="message-text">${message}</p
            `;
    document.getElementById("messagesList").appendChild(messegeDiv);

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

    if (!roomName) {
        connection.invoke("SendMessageToAll", message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    if (roomName) {
        connection.invoke("SendMessageToRoom", roomName, currentRoomId, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
}

function getRoomById(id, name) {
    connection.invoke("JoinRoom", id, name).catch(function (err) {
        return console.error(err.toString());
    });
    $('#roomName').text(name);
    document.getElementById("messagesList").innerHTML = '';
    $('.jumbotron').hide();
    currentRoomId = id;

}
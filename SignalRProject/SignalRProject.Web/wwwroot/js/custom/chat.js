"use strict";
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

connection.on('Notify', function (message) {
    let notifyElem = document.createElement("b");
    notifyElem.appendChild(document.createTextNode(message));
    let elem = document.createElement("p");
    elem.appendChild(notifyElem);
    var firstElem = document.getElementById("messagesList").firstChild;
    document.getElementById("messagesList").insertBefore(elem, firstElem);
});

connection.on("ReceiveMessage", function (user, message) {

    let today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    const yyyy = today.getFullYear();
    const how = today.getHours();
    const min = today.getMinutes();
    debugger
    today = `${mm}/${dd}/${yyyy} ${how}:${min}`;
    const messegeDiv = document.createElement('div');
    messegeDiv.className = 'message';

    messegeDiv.innerHTML = `
            <div class="nick-name">${user}</div>
            <img class="avatar" src="https://placeimg.com/50/50/people?1">
            <div class="datetime">${today}</div>
            <p class="message-text">${message}</p
            `;
    document.getElementById("messagesList").appendChild(messegeDiv);

});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = userEmail;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

window.setInterval(function () {
    var elem = document.getElementsByClassName('chat-container')[0];
    elem.scrollTop = elem.scrollHeight;
}, 5000);

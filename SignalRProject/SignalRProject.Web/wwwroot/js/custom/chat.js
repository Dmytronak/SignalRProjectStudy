"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {

    let today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    const yyyy = today.getFullYear();
    const how = today.getHours();
    const min = today.getMinutes();

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

    /* Auto scroll */
    $(".chat-container").stop().animate({
        scrollTop: $('.chat-container')[0].scrollHeight
    }, 1000);
    /* Auto scroll */

});
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
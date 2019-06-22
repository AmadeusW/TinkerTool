"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/trace").build();

connection.on("setProperty", function (name, value) {
    var li = document.createElement("li");
    li.textContent = name + " := " + value;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("property", function (name, value) {
    var li = document.createElement("li");
    li.textContent = name + " is " + value;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var name = document.getElementById("nameInput").value;
    var value = document.getElementById("valueInput").value;
    connection.invoke("setProperty", name, value).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

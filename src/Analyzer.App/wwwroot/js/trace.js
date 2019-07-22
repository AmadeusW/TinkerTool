"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/trace").build();

// From the TraceHub
connection.on("set", function (name, value) {
    var li = document.createElement("li");
    li.textContent = `${name} = ${value}`;
    document.getElementById("messagesList").appendChild(li);
});
connection.on("trace", function (name, value, caller, fileName, lineNumber, timestamp) {
    var li = document.createElement("li");
    li.textContent = `${fileName}:${lineNumber} [${caller}] ${name} : ${value}`;
    document.getElementById("messagesList").appendChild(li);
});
connection.on("log", function (name, value, timestamp) {
    var li = document.createElement("li");
    li.textContent = `${timestamp} [${name}] ${value}`;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var name = document.getElementById("nameInput").value;
    var value = document.getElementById("valueInput").value;
    connection.invoke("set", name, value).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

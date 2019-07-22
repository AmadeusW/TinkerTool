"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/trace").build();

var app = new Vue({
    el: '#app',
    data: {
        status: 'ok',
        dataTable: [],
        traceTable: [],
        logTable: []
    }
});

// Handle communication with the TraceHub
connection.on("set", function (name, value, timestamp) {
    app.$data.dataTable.push({ name: name, value: value, timestamp: timestamp });
});
connection.on("trace", function (name, value, caller, fileName, lineNumber, threadId, timestamp) {
    app.$data.traceTable.push({ name: name, value: value, caller: caller, fileName: fileName, lineNumber: lineNumber, threadId: threadId, timestamp: timestamp });
});
connection.on("log", function (name, value, timestamp) {
    app.$data.logTable.push({ name: name, value: value, timestamp: timestamp });
});

// Hook up the tables
var dataOptions = {
    valueNames: ['name'],
    item: '<li><span class="name"></span>: <span class="value"></span></li>'
};
var logOptions = {
    valueNames: ['name'],
    item: '<li><span class="timespan"></span> [<span class="name"></span>] <span class="value"></span></li>'
};

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var name = document.getElementById("nameInput").value;
    var value = document.getElementById("valueInput").value;
    connection.invoke("setInUi", name, value).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

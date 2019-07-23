"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/trace").build();

var app = new Vue({
    el: '#app',
    data: {
        status: 'ok',
        // Displayed data
        dataTable: [],
        traceTable: [],
        logTable: [],
        // Filtering for the data
        dataFilter: '',
        traceFilter: '',
        logFilter: '',
        // Setting
        nameToSet: '',
        valueToSet: '',
        // Graphing
        nameToGraph: '',
        chartVisible: false
    },
    methods: {
        filter: function (data, filter) {
            return data.filter(function (item) {
                return filter.trim().length === 0
                    || item.name.toString().includes(filter)
                    || item.value.toString().includes(filter);
            });
        },
        setClicked: function () {
            connection.invoke(
                "setInUi",
                app.$data.nameToSet,
                app.$data.valueToSet)
            .catch(function (err) {
                return console.error(err.toString());
            });
        },
        graphClicked: function () {
            var smoothie = new SmoothieChart();
            smoothie.streamTo(document.getElementById("chartCanvas"));
            chartData = new TimeSeries();
            app.$data.chartVisible = true;
            smoothie.addTimeSeries(chartData);
        }
    }
});

var chartData = {};

// Handle communication with the TraceHub
connection.on("set", function (name, value, timestamp) {
    app.$data.dataTable.push({ name: name, value: value, timestamp: timestamp });
    if (app.$data.chartVisible) {
        chartData.append(timestamp, value);
    }
});
connection.on("trace", function (name, value, caller, fileName, lineNumber, threadId, timestamp) {
    app.$data.traceTable.push({ name: name, value: value, caller: caller, fileName: fileName, lineNumber: lineNumber, threadId: threadId, timestamp: timestamp });
});
connection.on("log", function (name, value, timestamp) {
    app.$data.logTable.push({ name: name, value: value, timestamp: timestamp });
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
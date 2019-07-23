"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/trace").build();

var app = new Vue({
    el: '#app',
    data: {
        status: 'ok',
        // Displayed data
        dataTable: [], // may need to use Blazor to use a hashset
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
        keyToGraph: '',
        chartVisible: false
    },
    methods: {
        filter: function (data, filter) {
            return data.filter(function (item) {
                return filter.trim().length === 0
                    || item.toString().includes(filter)
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
        graphClicked: function (event) {          
            app.$data.keyToGraph = event.currentTarget.dataset.key;

            var smoothie = new SmoothieChart({ millisPerPixel: 100 });
            smoothie.streamTo(document.getElementById("chartCanvas"));

            chartData = new TimeSeries();
            chartData.append(app.$data.dataTable[app.$datakeyToGraph]);
            smoothie.addTimeSeries(chartData);

            app.$data.chartVisible = true;
        }
    }
});

var chartData = {};

// Handle communication with the TraceHub
connection.on("set", function (name, value, timestamp) {
    app.$data.dataTable.push({ name: name, value: value, timestamp: timestamp });
    if (app.$data.keyToGraph === name) {
        var jsTime = Date.parse(timestamp);
        chartData.append(jsTime, value);
    }
});
connection.on("trace", function (name, value, caller, fileName, lineNumber, threadId, timestamp) {
    app.$data.traceTable.push({ name: name, value: value, caller: caller, fileName: fileName, lineNumber: lineNumber, threadId: threadId, timestamp: timestamp });
    app.$data.dataTable.push({ name: name, value: value, uid: `{fileName}:{lineNumber}` })
});
connection.on("log", function (name, value, timestamp) {
    app.$data.logTable.push({ name: name, value: value, timestamp: timestamp });
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
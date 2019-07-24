using Analyzer.Library.Infrastructure;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Analyzer.Library
{
    internal class Transmitter
    {
        static Transmitter _instance;
        private ICallbackClient _callback;
        private AsyncLazy<HubConnection> connection;

        static internal Transmitter Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Transmitter();
                return _instance;
            }
        }

        internal void RegisterCallback(ICallbackClient callback)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        private Transmitter()
        {
            CreateNewConnection();
        }

        private void CreateNewConnection()
        {
            connection = new AsyncLazy<HubConnection>(async () =>
            {
                var c = new HubConnectionBuilder()
                .WithUrl("https://localhost:44341/trace")
                .Build();

                c.On<string, string>("broadcastMessage", OnBroadcast);
                // Response to the GET request
                c.On<string, string>("property", OnProperty);
                // Response to user setting a value in the web UI
                c.On<string, string>("setInUi", OnSet);
                await c.StartAsync();
                c.Closed += (exception) =>
                {
                    _callback.ReceiveInformation("Status", "Connection lost");
                    CreateNewConnection();
                    return Task.CompletedTask;
                };

                _callback.ReceiveInformation("Status", "Connected");
                return c;
            });
        }

        internal void SendGet(string name)
        {
            RunSafely(async () =>
            {
                await (await connection).InvokeAsync("get", name);
            });
        }

        internal void SendSet(string name, string value, DateTime timestamp)
        {
            RunSafely(async () =>
            {
                await (await connection).InvokeAsync("set", name, value, timestamp);
            });
        }

        internal void SendLog(string name, string value, DateTime timestamp)
        {
            RunSafely(async () =>
            {
                await (await connection).InvokeAsync("log", name, value, timestamp);
            });
        }

        internal void SendTrace(string name, string value, string caller, string fileName, int lineNumber, int threadId, DateTime timestamp)
        {
            RunSafely(async () =>
            {
                await (await connection).InvokeAsync("trace", name, value, caller, fileName, lineNumber, threadId, timestamp);
            });
        }

        private void OnBroadcast(string arg1, string arg2)
        {
            _callback.ReceiveInformation(arg1, arg2);
        }

        private void OnProperty(string name, string value)
        {
            _callback.NameValueChanged(name, value);
        }

        private void OnSet(string name, string value)
        {
            _callback.NameValueChanged(name, value);
        }

        private void RunSafely(Action action)
        {
            Task.Run(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    _callback.ReceiveError(ex);
                }
            });
        }
    }
}
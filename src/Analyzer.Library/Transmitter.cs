using Analyzer.Library.Infrastructure;
using Microsoft.AspNetCore.SignalR.Client;
using System;
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
                .WithUrl("https://localhost:44343/trace")
                .Build();

                c.On<string, string>("broadcastMessage", OnBroadcast);
                // Response to the GET request
                c.On<string, string>("property", OnProperty);
                // To be notified wenever a variable is set on the server, listen to "set"
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

        internal void Post(string method, string arg1)
        {
            RunSafely(async () =>
            {
                await (await connection).InvokeAsync(method, arg1);
            });
        }

        internal void Post(string method, string arg1, string arg2)
        {
            RunSafely(async () =>
            {
                await (await connection).InvokeAsync(method, arg1, arg2);
            });
        }

        internal void Post(string method, string arg1, string arg2, string arg3, string arg4, string arg5)
        {
            RunSafely(async () =>
            {
                await (await connection).InvokeAsync(method, arg1, arg2, arg3, arg4, arg5);
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

        internal void RunSafely(Action action)
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
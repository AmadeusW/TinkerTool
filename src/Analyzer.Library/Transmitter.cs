using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Analyzer.Library
{
    internal class Transmitter
    {
        static Transmitter _instance;
        private bool _started;
        private readonly HubConnection _connection;

        static Transmitter Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Transmitter();
                return _instance;
            }
        }

        private Transmitter()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44343/trace")
                .Build();

            _connection.On<string, string>("broadcastMessage", OnBroadcast);
        }

        private void OnBroadcast(string arg1, string arg2)
        {
            throw new NotImplementedException();
        }

        private async Task Connect()
        {
            if (_started)
                return;

            try
            {
                // TODO: Add thread safety to protect from calling StartAsync multiple times
                // For example, koin the task which returns active connection
                await _connection.StartAsync();
                _started = true;
            }
            catch (Exception ex)
            {
                var message = ex.ToString();
            }
        }

        internal static void Post(LoggedData data)
        {
            Task.Run(async () =>
            {
                try
                {
                    await Instance.PostAsyncAndForget(data);
                }
                catch (Exception ex)
                {
                    var message = ex.ToString();
                }
            });
        }

        internal async Task PostAsyncAndForget(LoggedData data)
        {
            await Connect();
            await _connection.InvokeAsync("Sample", data.ToString());
            await _connection.InvokeAsync("SendMessage", "yoo", data.value.ToString());
        }
    }
}
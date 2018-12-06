using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using System;
using System.Threading.Tasks;

namespace Analyzer.Library
{
    internal class Transmitter
    {
        static Transmitter _instance;
        static Transmitter Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Transmitter();
                return _instance;
            }
        }

        public Connection connection { get; }

        private Transmitter()
        {
            // umm i'm getting 405 when trying to start the connection.
            // how do I conect?
            // see https://docs.microsoft.com/en-us/aspnet/signalr/overview/getting-started/supported-platforms#client-system-requirements
            connection = new Connection("https://localhost:44343/trace");
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
            await connection.Start();
            await connection.Send(data.ToString());
        }
    }
}
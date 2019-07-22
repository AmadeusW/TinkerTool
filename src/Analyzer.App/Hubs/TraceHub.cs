using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analyzer.App.Hubs
{
    public class TraceHub : Hub
    {
        // Share properties because for every request we get a new instance of this type.
        static Dictionary<string, string> Properties = null;

        public TraceHub()
        {
            if (Properties == null)
                Properties = new Dictionary<string, string>();
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("TraceHub.SendMessage", user, message);
            await Clients.All.SendAsync("broadcastMessage", user, message);
        }

        public async Task get(string name)
        {
            if (Properties.TryGetValue(name, out var value))
            {
                await Clients.Caller.SendAsync("property", name, value);
            }
            else
            {
                await Clients.Caller.SendAsync("property", name, string.Empty);
            }
        }

        public async Task set(string name, string value, string timestamp)
        {
            // Value is set through the API
            Properties[name] = value;
            await Clients.All.SendAsync("set", name, value, timestamp);
        }

        public async Task setInUi(string name, string value)
        {
            // Value is set in the web UI
            Properties[name] = value;
            await Clients.All.SendAsync("setInUi", name, value);
        }

        public async Task log(string name, string value, string timestamp)
        {
            await Clients.All.SendAsync("log", name, value, timestamp);
        }

        public async Task trace(string name, string value, string caller, string fileName, int lineNumber, int threadId, string timestamp)
        {
            await Clients.All.SendAsync("trace", name, value, caller, fileName, lineNumber, threadId, timestamp);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}

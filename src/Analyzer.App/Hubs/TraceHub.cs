using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analyzer.App.Hubs
{
    public class TraceHub : Hub
    {
        Dictionary<string, string> Properties = new Dictionary<string, string>();

        public TraceHub()
        {

        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("TraceHub.SendMessage", user, message);
            await Clients.All.SendAsync("broadcastMessage", user, message);
        }

        public async Task getProperty(string name)
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

        public async Task setProperty(string name, string value)
        {
            Properties[name] = value;
            await Clients.All.SendAsync("property", name, value);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}

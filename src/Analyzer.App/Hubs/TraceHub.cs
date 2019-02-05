using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analyzer.App.Hubs
{
    public class TraceHub : Hub
    {
        public TraceHub()
        {

        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("TraceHub.SendMessage", user, message);
            await Clients.All.SendAsync("broadcastMessage", user, message);
        }

        public async Task Sample(string data)
        {
            await Clients.All.SendAsync("sample", data);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}

using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Analyzer.Blazor.Data;

namespace Analyzer.Blazor.Hubs
{
    public class TraceHub : Hub
    {
        private DataService dataService;

        public TraceHub(DataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("TraceHub.SendMessage", user, message);
            await Clients.All.SendAsync("broadcastMessage", user, message);
        }

        public async Task get(string name)
        {
            if (dataService.TryGetValue(name, out var value))
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
            dataService.Set(name, value);
            //await Clients.All.SendAsync("set", name, value, timestamp);
        }

        /*
        public async Task setInUi(string name, string value)
        {
            // Value is set in the web UI
            Properties[name] = value;
            await Clients.All.SendAsync("setInUi", name, value);
        }*/

        public async Task log(string name, string value, string timestamp)
        {
            dataService.Log(name, value, timestamp);
            //await Clients.All.SendAsync("log", name, value, timestamp);
        }

        public async Task trace(string name, string value, string caller, string fileName, int lineNumber, int threadId, string timestamp)
        {
            dataService.Trace(name, value, caller, fileName, lineNumber, threadId, timestamp);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}

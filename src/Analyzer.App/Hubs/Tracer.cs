using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Analyzer.App.Hubs
{
    public class Tracer : Hub
    {
        public Task Send(string data)
        {
            return Clients.All.InvokeAsync("Send", data);
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Analyzer.App.Models;

namespace Analyzer.App.Hubs
{
    public class Tracer : Hub
    {
        public Task Send(string data)
        {
            if (data.Contains("="))
            {
                var parsed = data.Split("=");
                if (parsed.Length == 2 && !string.IsNullOrWhiteSpace(parsed[0]))
                {
                    var property = parsed[0].Trim();
                    var value = parsed[1].Trim();
                    Repository.Set(property, value);
                    return Clients.All.InvokeAsync("Send", $"[{property}] := [{value}]");
                }
            }
            return Clients.All.InvokeAsync("Send", data);
        }
    }
}
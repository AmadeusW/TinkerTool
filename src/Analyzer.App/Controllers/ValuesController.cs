using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Analyzer.App.Hubs;

namespace Analyzer.App.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // https://stackoverflow.com/questions/7549179/signalr-posting-a-message-to-a-hub-via-an-action-method
        //private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<Tracer>();

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            //hubContext.Clients.Group("trace").addMessage($"Message: {id}");
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

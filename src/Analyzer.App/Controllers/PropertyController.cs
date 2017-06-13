using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Analyzer.App.Hubs;
using Analyzer.App.Models;

namespace Analyzer.App.Controllers
{
    [Route("property")]
    public class PropertyController : Controller
    {
        // https://stackoverflow.com/questions/7549179/signalr-posting-a-message-to-a-hub-via-an-action-method
        //private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<Tracer>();

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return null;
        }

        // GET api/values/5
        [HttpGet("{property}")]
        public string Get(string property)
        {
            return Repository.Get(property)?.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string property, [FromBody]string value)
        {
            Repository.Set(property, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

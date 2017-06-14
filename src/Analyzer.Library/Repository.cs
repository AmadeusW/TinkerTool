using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Library
{
    public class Repository : IRepository
    {
        ConcurrentDictionary<string, object> cache;

        public Repository()
        {
            cache = new ConcurrentDictionary<string, object>();
        }

        public void Initialize()
        {
            // TODO: subscribe to updates using signalr
        }

        public void Pull()
        {
            // TODO: get all
        }

        public object Get(string property)
        {
            object result;
            if (cache.TryGetValue(property , out result))
            {
                return result;
            }
            else
            {
                // TODO: Do a fast path
                result = GetInner(property).Result;
                cache[property] = result;
                return result;
            }
        }

        public void Set(string property, object value)
        {
            cache[property] = value;
            // TODO: Enqueue an update
        }

        private async Task<string> GetInner(string property)
        {
            // TODO: get using sockets (signalr)
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://localhost:5000/property/");
                    var response = await client.GetAsync(property);
                    response.EnsureSuccessStatusCode();

                    var stringResponse = await response.Content.ReadAsStringAsync();
                    return stringResponse;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request exception: {e.Message}");
                    return null;
                }
            }
        }
    }
}

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

        public void Pull()
        {
            // TODO: get all
        }

        public bool TryGet(string name, out object value)
        {
            return cache.TryGetValue(name, out value);
        }

        public void Set(string name, object value)
        {
            cache[name] = value;
        }

        public bool TryChange(string name, object value)
        {
            var currentValue = cache.ContainsKey(name) ? cache[name] : null;
            cache[name] = value;
            return value != currentValue;
        }
    }
}

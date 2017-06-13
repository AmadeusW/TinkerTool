using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analyzer.App.Models
{
    public static class Repository
    {
        static Dictionary<string, object> data;

        internal static void Start()
        {
            data = new Dictionary<string, object>(); // TODO: Load from persistent storage
        }

        internal static void Set(string property, object value)
        {
            data[property] = value;
        }

        internal static object Get(string property)
        {
            object result;
            if (data.TryGetValue(property, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

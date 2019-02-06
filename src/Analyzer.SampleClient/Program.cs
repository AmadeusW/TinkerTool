using Analyzer.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.SampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var tracer = new Tracer(new CallbackClient());
            while (true)
            {
                var input = Console.ReadLine().Trim();
                if (input.ToLowerInvariant() == "exit")
                    return;

                if (input.ToLowerInvariant().StartsWith("get "))
                {
                    var split = input.Split(' ');
                    var name = split[1];
                    tracer.Get(name);
                    continue;
                }

                if (input.ToLowerInvariant().StartsWith("set "))
                {
                    var split = input.Split(' ');
                    var name = split[1];
                    var value = split[2];
                    tracer.Set(name, value);
                    continue;
                }

                tracer.Post(input);
            }
        }
    }

    class CallbackClient : ICallbackClient
    {
        public void Log(string category, string message)
        {
            Console.WriteLine(string.IsNullOrEmpty(category)
                ? message
                : $"{category}: {message}");
        }

        public void LogError(Exception ex)
        {
            Log(ex.GetType().ToString(), ex.Message);
        }
    }
}

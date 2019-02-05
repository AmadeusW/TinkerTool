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
                var input = Console.ReadLine();
                if (input.ToLowerInvariant() == "exit")
                    return;

                if (input.ToLowerInvariant().StartsWith("="))
                {
                    tracer.Post("equals", "character");
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

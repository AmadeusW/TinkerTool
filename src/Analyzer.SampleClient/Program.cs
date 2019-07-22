using Analyzer.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                var inputs = input.Split(' ');

                if (inputs.Length == 0)
                    continue;

                var command = inputs[0];
                var name = inputs.Length >= 2 ? inputs[1] : string.Empty;
                var value = inputs.Length >= 3 ? inputs[2] : string.Empty;

                switch (command)
                {
                    case "exit":
                        return;
                    case "get":
                        tracer.Get(name);
                        break;
                    case "set":
                        tracer.Set(name, value);
                        break;
                    case "log":
                        tracer.Log(name, value);
                        break;
                    case "call":
                        tracer.Trace("command", "call");
                        break;
                    default:
                        Console.WriteLine($"Unknown command {command}");
                        break;
                }
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

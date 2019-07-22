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
            var tracer = new Tracer();
            tracer.OnDiagnosticMessage += (o, e) => Console.WriteLine($"{e.Name}: {e.Value}");
            tracer.OnError += (o, e) => Console.WriteLine($"!{e.Name}: {e.Value}");
            tracer.OnValueChanged += (o, e) => Console.WriteLine($"{e.Name} = {e.Value}");

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
                        var getValue = tracer.Get(name);
                        Console.WriteLine(getValue);
                        break;
                    case "set":
                        tracer.Set(name, value);
                        break;
                    case "log":
                        tracer.Log(name, value);
                        break;
                    case "trace":
                        tracer.Trace("command", "call");
                        break;
                    default:
                        Console.WriteLine($"Unknown command {command}");
                        break;
                }
            }
        }
    }
}

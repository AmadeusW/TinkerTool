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
            var tracer = new Tracer();
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
}

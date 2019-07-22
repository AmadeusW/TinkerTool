using System;
using System.Runtime.CompilerServices;

namespace Analyzer.Library
{
    public class Tracer : ITracer
    {
        private Lazy<Transmitter> transmitter;

        public Tracer(ICallbackClient callbackClient)
        {
            transmitter = new Lazy<Transmitter>(() =>
            {
                var transmitter = Transmitter.Instance;
                transmitter.RegisterCallback(callbackClient);
                return transmitter;
            });
        }

        public void Trace(object value, string name = "", [CallerMemberName] string caller = "", [CallerFilePath] string file = "", [CallerLineNumber] int lineNumber = 0)
        {
            transmitter.Value.Post("trace", name, value.ToString(), caller, file, lineNumber.ToString());
        }

        public void Get(string name)
        {
            transmitter.Value.Post("get", name);
        }

        public void Set(string name, string value)
        {
            transmitter.Value.Post("set", name, value);
        }

        public void Log(string name, string value)
        {
            transmitter.Value.Post("log", name, value);
        }
    }
}
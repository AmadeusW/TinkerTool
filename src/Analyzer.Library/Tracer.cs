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

        public void Post(object value, string name = "", [CallerMemberName] string caller = "", [CallerFilePath] string file = "", [CallerLineNumber] int lineNumber = 0)
        {
            /*
            var data = new LoggedData(value, name, caller, file, lineNumber);
            transmitter.Value.Post(data);
            */
        }

        public void Get(string name)
        {
            transmitter.Value.Post("getProperty", name);
        }

        public void Set(string name, string value)
        {
            transmitter.Value.Post("setProperty", name, value);
        }

        public void Log(string name, string value)
        {
            throw new NotImplementedException();
        }
    }
}
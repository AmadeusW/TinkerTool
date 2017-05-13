using System;
using System.Runtime.CompilerServices;

namespace Analyzer.Library
{
    public class Tracer : ITracer
    {
        public void Post(object value)
        {
            var data = new LoggedData(value);
        }

        public void Post(object value, string name = "")
        {
            var data = new LoggedData(value, name);
        }

        public void Post(object value, string name = "", [CallerMemberName] string caller = "", [CallerFilePath] string file = "", [CallerLineNumber] int lineNumber = 0)
        {
            var data = new LoggedData(value, name, caller, file, lineNumber);
        }
    }
}
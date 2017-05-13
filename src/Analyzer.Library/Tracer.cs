using System;
using System.Runtime.CompilerServices;

namespace Analyzer.Library
{
    public class Tracer : ITracer
    {
        public void Post(object value)
        {
            throw new NotImplementedException();
        }

        public void Post(object value, string name = "")
        {
            throw new NotImplementedException();
        }

        public void Post(object value, string name = "", [CallerMemberName] string caller = "", [CallerFilePath] string file = "", [CallerLineNumber] int lineNumber = 0)
        {
            throw new NotImplementedException();
        }
    }
}
using System.Runtime.CompilerServices;

namespace Analyzer.Library
{
    /// <summary>
    /// Object which interacts with user code
    /// </summary>
    public interface ITracer
    {
        void Get(string name);
        void Set(string name, string value);
        //void Post(object value);
        //void Post(object value, string name = "");
        void Trace(object value, string name = "", [CallerMemberNameAttribute] string caller = "", [CallerFilePathAttribute] string file = "", [CallerLineNumberAttribute] int lineNumber = 0);
    }
}

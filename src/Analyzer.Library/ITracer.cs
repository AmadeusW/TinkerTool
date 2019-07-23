using System;
using System.Runtime.CompilerServices;

namespace Analyzer.Library
{
    /// <summary>
    /// Object which interacts with user code
    /// </summary>
    public interface ITracer
    {
        object Get(string name);
        void Set(string name, string value);
        void ReceiveInformation(string name, string value);
        void Trace(string name, object value, [CallerMemberNameAttribute] string caller = "", [CallerFilePathAttribute] string file = "", [CallerLineNumberAttribute] int lineNumber = 0);

        event EventHandler<NameValueEventArgs> OnValueChanged;
        event EventHandler<NameValueEventArgs> OnError;
        event EventHandler<NameValueEventArgs> OnDiagnosticMessage;
    }
}

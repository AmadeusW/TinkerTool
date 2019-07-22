using System;
using System.Collections.Generic;
using System.Text;

namespace Analyzer.Library
{
    public interface ICallbackClient
    {
        void ReceiveError(Exception ex);
        void ReceiveInformation(string category, string message);
        void NameValueChanged(string name, string value);
    }

    public class NameValueEventArgs : EventArgs
    {
        public string Name { get; }
        public object Value { get; }

        public NameValueEventArgs(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}

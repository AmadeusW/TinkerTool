using System;
using System.Collections.Generic;
using System.Text;

namespace Analyzer.Library
{
    public interface ICallbackClient
    {
        void LogError(Exception ex);
        void Log(string category, string message);
    }
}

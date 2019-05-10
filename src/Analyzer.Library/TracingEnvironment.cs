using System;
using System.Collections.Generic;
using System.Text;

namespace Analyzer.Library
{
    internal class TracingEnvironment
    {
        // Client side view ofthe data model
        internal IRepository Repository { get; private set; }
        // Client library interface
        internal ITracer Tracer { get; private set; }
        // TODO what's this?
        internal ICallbackClient CallbackClient { get; private set; }

        public TracingEnvironment()
        {

        }
    }
}

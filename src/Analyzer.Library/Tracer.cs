using System;
using System.Runtime.CompilerServices;

namespace Analyzer.Library
{
    public class Tracer : ITracer, ICallbackClient
    {
        private Lazy<Transmitter> transmitter;
        private IRepository repository;

        public event EventHandler<NameValueEventArgs> OnValueChanged;
        public event EventHandler<NameValueEventArgs> OnError;
        public event EventHandler<NameValueEventArgs> OnDiagnosticMessage;

        public Tracer()
        {
            transmitter = new Lazy<Transmitter>(() =>
            {
                var transmitter = Transmitter.Instance;
                transmitter.RegisterCallback(this);
                return transmitter;
            });
            repository = new Repository();
        }

        public void Trace(object value, string name = "", [CallerMemberName] string caller = "", [CallerFilePath] string file = "", [CallerLineNumber] int lineNumber = 0)
        {
            transmitter.Value.Post("trace", name, value.ToString(), caller, file, lineNumber.ToString());
        }

        public object Get(string name)
        {
            if (repository.TryGet(name, out var value))
            {
                return value;
            }
            else
            {
                transmitter.Value.Post("get", name);
                return null;
            }
        }

        public void Set(string name, string value)
        {
            repository.Set(name, value);
            transmitter.Value.Post("set", name, value);
        }

        public void ReceiveInformation(string name, string value)
        {
            OnDiagnosticMessage(this, new NameValueEventArgs(name, value));
            transmitter.Value.Post("log", name, value);
        }

        public void ReceiveError(Exception ex)
        {
            OnError?.Invoke(this, new NameValueEventArgs(ex.GetType().ToString(), ex));
        }

        public void NameValueChanged(string name, string value)
        {
            if (repository.TryChange(name, value))
            {
                OnValueChanged(this, new NameValueEventArgs(name, value));
            }
        }
    }
}
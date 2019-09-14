using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Analyzer.Blazor.Data
{
    public class DataService
    {
        // Share properties because for every request we get a new instance of this type.
        static Dictionary<DataId, LoggedData> Data = null;

        public Func<string, string, Task> SetInUiHandler { get; internal set; }

        public DataService()
        {
            // TODO: See whether this is called just once
            // or for every request
            Data = new Dictionary<DataId, LoggedData>();
        }

        public event EventHandler OnDataUpdated;

        internal bool TryGetValue(string name, out object value)
        {
            var key = new DataId(name);
            if (Data.TryGetValue(key, out var data))
            {
                value = data.Value;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        public LoggedData[] GetData()
        {
            return Data.Values.ToArray();
        }

        internal void Trace(string name, string value, string caller, string fileName, int lineNumber, int threadId, string timestamp)
        {
            var key = new DataId(name, caller, fileName, lineNumber, threadId);
            Data[key] = new LoggedData
            {
                Value = value,
                TimeStamp = DateTime.Parse(timestamp),
                Id = key,
            };
            OnDataUpdated?.Invoke(this, EventArgs.Empty);
        }

        internal void Log(string name, string value, string timestamp)
        {
            var key = new DataId(name);
            Data[key] = new LoggedData
            {
                Value = value,
                TimeStamp = DateTime.Parse(timestamp),
                Id = key,
            };
            OnDataUpdated?.Invoke(this, EventArgs.Empty);
        }

        internal void Set(string name, string value)
        {
            SetCore(name, value);
            OnDataUpdated?.Invoke(this, EventArgs.Empty);
        }

        internal void SetInUi(string name, string value)
        {
            SetCore(name, value);
            Task.Run(async () => await SetInUiHandler(name, value));
        }

        private void SetCore(string name, string value)
        {
            var key = new DataId(name);
            Data[key] = new LoggedData
            {
                Value = value,
                TimeStamp = DateTime.Now,
                Id = key,
            };
        }
    }
}

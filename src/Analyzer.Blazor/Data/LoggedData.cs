using System;

namespace Analyzer.Blazor.Data
{
    public class LoggedData
    {
        public DateTime TimeStamp { get; set; }

        public object Value { get; set; }

        public DataId Id { get; set; }

        public string Name => Id.Name;
    }
}

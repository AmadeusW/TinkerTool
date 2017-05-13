namespace Analyzer.Library
{
    internal class LoggedData
    {
        private object value;
        private string name;
        private string caller;
        private string file;
        private int lineNumber;

        public LoggedData(object value)
        {
            this.value = value;
        }

        public LoggedData(object value, string name) : this(value)
        {
            this.name = name;
        }

        public LoggedData(object value, string name, string caller, string file, int lineNumber) : this(value, name)
        {
            this.caller = caller;
            this.file = file;
            this.lineNumber = lineNumber;
        }
    }
}
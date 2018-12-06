namespace Analyzer.Library
{
    internal class LoggedData
    {
        private object value;
        private string name;
        private string caller;
        private string file;
        private int lineNumber;

        private string test;

        public LoggedData(object value)
        {
            this.value = value;

            test = value.ToString();
        }

        public LoggedData(object value, string name) : this(value)
        {
            this.name = name;

            test = $"{name}={value}";
        }

        public LoggedData(object value, string name, string caller, string file, int lineNumber) : this(value, name)
        {
            this.caller = caller;
            this.file = file;
            this.lineNumber = lineNumber;

            test = $"{file}:{lineNumber} - {caller} - {name}={value}";
        }

        public override string ToString()
        {
            return test;
        }
    }
}
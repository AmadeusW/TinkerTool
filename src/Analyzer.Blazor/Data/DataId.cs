using System;
using System.Diagnostics.CodeAnalysis;

namespace Analyzer.Blazor.Data
{
    public class DataId : IComparable<DataId>
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public string CallerName { get; set; }
        public int ThreadId;
        // TODO: add knowledge about the type: string, int, float, bool

        public DataId(string name)
        {
            Name = name;
        }

        public DataId(string name, string caller, string fileName, int lineNumber, int threadId) : this(name)
        {
            this.CallerName = caller;
            this.FileName = fileName;
            this.LineNumber = lineNumber;
            this.ThreadId = threadId;
        }

        public int CompareTo([AllowNull] DataId other)
        {
            var nameComparison = this.Name.CompareTo(other.Name);
            if (nameComparison != 0)
            {
                return nameComparison;
            }
            if (this.FileName != default || other.FileName != default)
            {
                var fileComparison = this.FileName.CompareTo(other.FileName);
                if (fileComparison == 0)
                {
                    return this.LineNumber.CompareTo(other.LineNumber);
                }
                else
                {
                    return fileComparison;
                }
            }
            // names are the same and there is no file path to fall back on
            return 0;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DataId other))
                return false;
            else if (FileName == default)
                return Name.Equals(other.Name);
            else
                return Name.Equals(other.Name) && FileName.Equals(other.FileName) && LineNumber.Equals(other.LineNumber);
        }

        public override int GetHashCode()
        {
            if (FileName == default)
                return Name.GetHashCode();
            else
                return (Name, FileName, LineNumber).GetHashCode();
        }

        internal bool MatchesNameOnly(string name)
        {
            return FileName == default && Name == name;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(FileName) ? Name : $"{Name} ({FileName}:{LineNumber})";
        }
    }
}
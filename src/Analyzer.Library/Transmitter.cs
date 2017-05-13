using System;

namespace Analyzer.Library
{
    internal class Transmitter
    {
        static Transmitter _instance;
        static Transmitter Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                else
                {
                    _instance = new Transmitter();
                    return _instance;
                }
            }
        }

        internal static void Post(LoggedData data)
        {
            throw new NotImplementedException();
        }
    }
}
using System;

namespace Konfiggy.Core.Exceptions
{
    public class KonfiggyFilePathNotSetException : Exception
    {
        public KonfiggyFilePathNotSetException(string msg) : base(msg)
        {
            
        }
    }
}

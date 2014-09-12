using System;

namespace KonfiggyFramework.Exceptions
{
    public class KonfiggyFilePathNotSetException : Exception
    {
        public KonfiggyFilePathNotSetException(string msg) : base(msg)
        {
            
        }
    }
}

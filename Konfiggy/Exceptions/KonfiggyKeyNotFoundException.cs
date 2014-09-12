using System;

namespace KonfiggyFramework.Exceptions
{
    public class KonfiggyKeyNotFoundException : Exception
    {
        public KonfiggyKeyNotFoundException(string msg, Exception innerException) : base(msg, innerException)
        {
            
        }

        public KonfiggyKeyNotFoundException(string msg) : base(msg)
        {
            
        }
    }
}
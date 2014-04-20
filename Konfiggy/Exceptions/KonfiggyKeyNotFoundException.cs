using System;

namespace Konfiggy.Exceptions
{
    public class KonfiggyKeyNotFoundException : Exception
    {
        public KonfiggyKeyNotFoundException(string msg) : base(msg)
        {
            
        }
    }
}
using System;

namespace Konfiggy.Exceptions
{
    public class KonfiggyEnvironmentNotSetException : Exception
    {
        public KonfiggyEnvironmentNotSetException(string msg) : base(msg)
        {
            
        }
    }
}
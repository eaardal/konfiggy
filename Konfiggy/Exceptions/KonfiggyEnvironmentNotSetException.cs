using System;

namespace Konfiggy.Core.Exceptions
{
    public class KonfiggyEnvironmentNotSetException : Exception
    {
        public KonfiggyEnvironmentNotSetException(string msg) : base(msg)
        {
            
        }
    }
}
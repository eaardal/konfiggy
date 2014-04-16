using System;

namespace Konfiggy.Exceptions
{
    public class KonfiggyNoConfigurationKeyFoundException : Exception
    {
        public KonfiggyNoConfigurationKeyFoundException(string msg) : base(msg)
        {
            
        }
    }
}
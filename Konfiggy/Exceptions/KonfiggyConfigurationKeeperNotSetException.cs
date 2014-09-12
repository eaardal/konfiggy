using System;

namespace Konfiggy.Core.Exceptions
{
    public class KonfiggyConfigurationKeeperNotSetException : Exception
    {
        public KonfiggyConfigurationKeeperNotSetException(string msg) : base(msg)
        {
            
        }
    }
}

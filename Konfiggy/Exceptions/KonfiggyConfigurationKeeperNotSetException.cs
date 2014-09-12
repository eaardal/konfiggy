using System;

namespace KonfiggyFramework.Exceptions
{
    public class KonfiggyConfigurationKeeperNotSetException : Exception
    {
        public KonfiggyConfigurationKeeperNotSetException(string msg) : base(msg)
        {
            
        }
    }
}

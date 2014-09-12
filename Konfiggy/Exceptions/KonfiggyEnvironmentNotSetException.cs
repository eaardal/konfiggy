using System;

namespace KonfiggyFramework.Exceptions
{
    public class KonfiggyEnvironmentNotSetException : Exception
    {
        public KonfiggyEnvironmentNotSetException(string msg) : base(msg)
        {
            
        }
    }
}
using System;

namespace Konfiggy.Exceptions
{
    public class KonfiggyEnvironmentTagNotFoundException : Exception
    {
        public KonfiggyEnvironmentTagNotFoundException(string msg) : base(msg)
        {
            
        }
    }
}
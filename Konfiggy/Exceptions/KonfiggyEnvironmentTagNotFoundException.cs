using System;

namespace Konfiggy.Core.Exceptions
{
    public class KonfiggyEnvironmentTagNotFoundException : Exception
    {
        public KonfiggyEnvironmentTagNotFoundException(string msg) : base(msg)
        {
            
        }
    }
}
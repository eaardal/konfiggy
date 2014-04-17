using System;

namespace Konfiggy.Exceptions
{
    public class KonfiggyConfigSectionNotSetException : Exception
    {
        public KonfiggyConfigSectionNotSetException(string msg) : base(msg)
    {

    }
    }
}

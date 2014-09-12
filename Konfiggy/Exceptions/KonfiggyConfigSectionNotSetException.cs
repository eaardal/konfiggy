using System;

namespace Konfiggy.Core.Exceptions
{
    public class KonfiggyConfigSectionNotSetException : Exception
    {
        public KonfiggyConfigSectionNotSetException(string msg) : base(msg)
    {

    }
    }
}

using System;

namespace Konfiggy.Core.Exceptions
{
    public class KonfiggyFileSettingsNotSetException : Exception
    {
        public KonfiggyFileSettingsNotSetException(string msg)
            : base(msg)
        {

        }
    }
}

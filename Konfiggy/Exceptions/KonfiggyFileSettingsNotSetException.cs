using System;

namespace KonfiggyFramework.Exceptions
{
    public class KonfiggyFileSettingsNotSetException : Exception
    {
        public KonfiggyFileSettingsNotSetException(string msg)
            : base(msg)
        {

        }
    }
}

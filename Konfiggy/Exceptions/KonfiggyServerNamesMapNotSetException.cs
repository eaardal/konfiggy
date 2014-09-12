using System;

namespace Konfiggy.Core.Exceptions
{
    public class KonfiggyServerNamesMapNotSetException : Exception
    {
        public KonfiggyServerNamesMapNotSetException(string msg)
            : base(msg)
        {

        }
    }
}

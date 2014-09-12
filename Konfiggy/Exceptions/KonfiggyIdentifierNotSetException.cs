using System;

namespace Konfiggy.Core.Exceptions
{
    public class KonfiggyIdentifierNotSetException : Exception
    {
        public KonfiggyIdentifierNotSetException(string msg)
            : base(msg)
        {

        }
    }
}

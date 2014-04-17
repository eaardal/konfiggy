using System;

namespace Konfiggy.Exceptions
{
    public class KonfiggyIdentifierNotSetException : Exception
    {
        public KonfiggyIdentifierNotSetException(string msg)
            : base(msg)
        {

        }
    }
}

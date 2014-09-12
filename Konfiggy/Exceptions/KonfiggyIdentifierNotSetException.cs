using System;

namespace KonfiggyFramework.Exceptions
{
    public class KonfiggyIdentifierNotSetException : Exception
    {
        public KonfiggyIdentifierNotSetException(string msg)
            : base(msg)
        {

        }
    }
}

using System;

namespace KonfiggyFramework.Exceptions
{
    public class KonfiggyServerNamesMapNotSetException : Exception
    {
        public KonfiggyServerNamesMapNotSetException(string msg)
            : base(msg)
        {

        }
    }
}

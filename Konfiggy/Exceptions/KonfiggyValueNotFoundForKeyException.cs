using System;

namespace KonfiggyFramework.Exceptions
{
    public class KonfiggyValueNotFoundForKeyException : Exception
    {
        public KonfiggyValueNotFoundForKeyException(string msg) : base(msg)
        {
            
        }
    }
}
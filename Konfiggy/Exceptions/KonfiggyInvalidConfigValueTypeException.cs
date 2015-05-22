using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonfiggyFramework.Exceptions
{
    public class KonfiggyInvalidConfigValueTypeException : Exception
    {
        public KonfiggyInvalidConfigValueTypeException(Exception innerException) : base(innerException.Message, innerException)
        {
            
        }

        public KonfiggyInvalidConfigValueTypeException(string msg, Exception innerException) : base(msg, innerException)
        {
            
        }
    }
}

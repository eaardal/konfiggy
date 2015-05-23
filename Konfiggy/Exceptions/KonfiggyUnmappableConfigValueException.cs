using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonfiggyFramework.Exceptions
{
    public class KonfiggyUnmappableConfigValueException : Exception
    {
        public KonfiggyUnmappableConfigValueException(string msg, Exception innerException) : base(msg, innerException)
        {
            
        }
    }
}

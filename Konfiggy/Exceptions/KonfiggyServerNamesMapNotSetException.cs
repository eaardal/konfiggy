using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konfiggy.Exceptions
{
    public class KonfiggyServerNamesMapNotSetException : Exception
    {
        public KonfiggyServerNamesMapNotSetException(string msg)
            : base(msg)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konfiggy.Exceptions
{
    public class KonfiggyNoTagStrategiesSetException : Exception
    {
        public KonfiggyNoTagStrategiesSetException(string msg) : base(msg)
        {
            
        }
    }
}

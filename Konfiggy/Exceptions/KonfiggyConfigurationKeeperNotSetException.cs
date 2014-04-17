using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Konfiggy.Exceptions
{
    public class KonfiggyConfigurationKeeperNotSetException : Exception
    {
        public KonfiggyConfigurationKeeperNotSetException(string msg) : base(msg)
        {
            
        }
    }
}

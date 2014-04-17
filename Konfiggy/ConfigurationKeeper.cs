using System.Collections.Specialized;
using System.Configuration;

namespace Konfiggy
{
    public class ConfigurationKeeper : IConfigurationKeeper
    {
        public NameValueCollection GetSection(string name)
        {
            return (NameValueCollection)ConfigurationManager.GetSection(name);
        }
    }
}

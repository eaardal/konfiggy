using System.Collections.Generic;
using System.Configuration;
using Konfiggy.Helpers;

namespace Konfiggy.KeyValueRetrievalStrategies
{
    public class ConnectionStringsRetrievalStrategy : IKeyValueRetrievalStrategy
    {
        public IDictionary<string, string> GetKeyValueCollection(IConfigurationKeeper configurationKeeper)
        {
            var configSection = (ConnectionStringsSection)configurationKeeper.GetSection("connectionStrings");
            var connectionStrings = configSection.ConnectionStrings;
            return Converters.ConvertToDictionary(connectionStrings);
        }
    }
}

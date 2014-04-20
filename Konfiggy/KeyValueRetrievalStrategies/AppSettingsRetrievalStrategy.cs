using System.Collections.Generic;
using System.Collections.Specialized;
using Konfiggy.Helpers;

namespace Konfiggy.KeyValueRetrievalStrategies
{
    public class AppSettingsRetrievalStrategy : IKeyValueRetrievalStrategy
    {
        public IDictionary<string, string> GetKeyValueCollection(IConfigurationKeeper configurationKeeper)
        {
            var appSettings = (NameValueCollection)configurationKeeper.GetSection("appSettings");
            return Converters.ConvertToDictionary(appSettings);
        }
    }
}

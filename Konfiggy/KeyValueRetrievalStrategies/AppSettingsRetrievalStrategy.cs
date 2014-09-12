using System.Collections.Generic;
using System.Collections.Specialized;
using Konfiggy.Core.Helpers;

namespace Konfiggy.Core.KeyValueRetrievalStrategies
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

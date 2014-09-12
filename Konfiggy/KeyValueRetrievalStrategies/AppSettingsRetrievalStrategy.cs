using System.Collections.Generic;
using System.Collections.Specialized;
using KonfiggyFramework.Helpers;

namespace KonfiggyFramework.KeyValueRetrievalStrategies
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

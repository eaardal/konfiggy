using System.Collections.Generic;

namespace KonfiggyFramework.KeyValueRetrievalStrategies
{
    public interface IKeyValueRetrievalStrategy
    {
        IDictionary<string, string> GetKeyValueCollection(IConfigurationKeeper configurationKeeper);
    }
}

using System.Collections.Generic;

namespace Konfiggy.Core.KeyValueRetrievalStrategies
{
    public interface IKeyValueRetrievalStrategy
    {
        IDictionary<string, string> GetKeyValueCollection(IConfigurationKeeper configurationKeeper);
    }
}

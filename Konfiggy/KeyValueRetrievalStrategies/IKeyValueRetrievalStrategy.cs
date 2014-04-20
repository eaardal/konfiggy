using System.Collections.Generic;
using Konfiggy.Helpers;

namespace Konfiggy.KeyValueRetrievalStrategies
{
    public interface IKeyValueRetrievalStrategy
    {
        IDictionary<string, string> GetKeyValueCollection(IConfigurationKeeper configurationKeeper);
    }
}

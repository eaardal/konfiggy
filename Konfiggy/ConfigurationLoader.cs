using System;
using KonfiggyFramework.KeyValueRetrievalStrategies;

namespace KonfiggyFramework
{
    public class ConfigurationLoader<T> where T: new()
    {
        private readonly IConfigurationKeeper _configurationKeeper;
        private readonly IKeyValueRetrievalStrategy _keyValueRetrievalStrategy;
        private readonly T _config;

        public ConfigurationLoader(IConfigurationKeeper configurationKeeper, IKeyValueRetrievalStrategy keyValueRetrievalStrategy)
        {
            if (configurationKeeper == null) throw new ArgumentNullException("configurationKeeper");
            if (keyValueRetrievalStrategy == null) throw new ArgumentNullException("keyValueRetrievalStrategy");
            _configurationKeeper = configurationKeeper;
            _keyValueRetrievalStrategy = keyValueRetrievalStrategy;
            
            _config = new T();
        }

        public T Load()
        {
            return _config;
        }

        public ConfigurationLoader<T> WithAppSettings()
        {
            var keyValues = _keyValueRetrievalStrategy.GetKeyValueCollection(_configurationKeeper);



            return this;
        }
    }
}
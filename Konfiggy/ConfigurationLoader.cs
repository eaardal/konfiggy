using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using KonfiggyFramework.Exceptions;
using KonfiggyFramework.KeyValueRetrievalStrategies;

namespace KonfiggyFramework
{
    public class ConfigurationLoader<T> : IConfigurationLoader<T> where T : new()
    {
        private readonly IConfigurationKeeper _configurationKeeper;
        private readonly IKeyValueRetrievalStrategy _appSettingsRetrievalStrategy;
        private readonly IKeyValueRetrievalStrategy _connectionStringsRetrievalStrategy;
        private readonly T _config;

        public ConfigurationLoader(IConfigurationKeeper configurationKeeper, IKeyValueRetrievalStrategy appSettingsRetrievalStrategy, IKeyValueRetrievalStrategy connectionStringsRetrievalStrategy)
        {
            if (configurationKeeper == null) throw new ArgumentNullException("configurationKeeper");
            if (appSettingsRetrievalStrategy == null) throw new ArgumentNullException("appSettingsRetrievalStrategy");
            if (connectionStringsRetrievalStrategy == null) throw new ArgumentNullException("connectionStringsRetrievalStrategy");

            _configurationKeeper = configurationKeeper;
            _appSettingsRetrievalStrategy = appSettingsRetrievalStrategy;
            _connectionStringsRetrievalStrategy = connectionStringsRetrievalStrategy;

            _config = new T();
        }

        public T Load()
        {
            return _config;
        }

        public ConfigurationLoader<T> WithAppSettings()
        {
            var keyValues = _appSettingsRetrievalStrategy.GetKeyValueCollection(_configurationKeeper);
            var keys = keyValues.Select(kvp => kvp.Key.ToLower(CultureInfo.InvariantCulture)).ToArray();

            var type = typeof(T);
            var properties = type.GetProperties().Where(p => p.CanRead && p.CanWrite);

            try
            {
                foreach (var property in properties)
                {
                    var propertyName = property.Name.ToLower(CultureInfo.InvariantCulture);

                    if (!keys.Contains(propertyName)) continue;

                    var val = keyValues.Single(k => k.Key.ToLower(CultureInfo.InvariantCulture) == propertyName).Value;

                    if (property.PropertyType == typeof(IEnumerable<string>))
                    {
                        property.SetValue(_config, val.Split(';'));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(val))
                        {
                            property.SetValue(_config, Convert.ChangeType(val, property.PropertyType));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new KonfiggyUnmappableConfigValueException("Could not map a key/value from appSettings", ex);
            }

            return this;
        }

        public ConfigurationLoader<T> WithConnectionStrings()
        {
            var keyValues = _connectionStringsRetrievalStrategy.GetKeyValueCollection(_configurationKeeper);
            var keys = keyValues.Select(kvp => kvp.Key.ToLower(CultureInfo.InvariantCulture)).ToArray();

            var type = typeof(T);
            var properties = type.GetProperties().Where(p => p.CanRead && p.CanWrite);

            try
            {
                foreach (var property in properties)
                {
                    var propertyName = property.Name.ToLower(CultureInfo.InvariantCulture);

                    if (!keys.Contains(propertyName)) continue;

                    var val = keyValues.Single(k => k.Key.ToLower(CultureInfo.InvariantCulture) == propertyName).Value;

                    if (!string.IsNullOrEmpty(val))
                    {
                        property.SetValue(_config, val);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new KonfiggyUnmappableConfigValueException("Could not map a key/value from connectionStrings", ex);
            }

            return this;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using KonfiggyFramework.Exceptions;

namespace KonfiggyFramework
{
    public class ConfigurationLoader<T> : IConfigurationLoader<T> where T : new()
    {
        private readonly IKonfiggy _konfiggy;
        private readonly T _config;

        public ConfigurationLoader(IKonfiggy konfiggy)
        {
            if (konfiggy == null) throw new ArgumentNullException("konfiggy");
            _konfiggy = konfiggy;

            _config = new T();
        }

        public T Load()
        {
            return _config;
        }

        public ConfigurationLoader<T> WithAppSettings()
        {
            var culture = CultureInfo.InvariantCulture;

            var properties = typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite).ToArray();

            foreach (var property in properties)
            {
                try
                {
                    var propertyName = property.Name.ToLower(culture);

                    var val = _konfiggy.GetAppSetting(propertyName);

                    if (string.IsNullOrEmpty(val)) continue;

                    if (property.PropertyType == typeof(IEnumerable<string>))
                    {
                        property.SetValue(_config, val.Split(';'));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(val))
                        {
                            property.SetValue(_config, Convert.ChangeType(val, property.PropertyType, culture));
                        }
                    }
                }
                catch (KonfiggyKeyNotFoundException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    throw new KonfiggyUnmappableConfigValueException("Could not map a key/value from appSettings", ex);
                }
            }

            return this;
        }

        public ConfigurationLoader<T> WithConnectionStrings()
        {
            var culture = CultureInfo.InvariantCulture;

            var properties = typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite).ToArray();

            foreach (var property in properties)
            {
                try
                {
                    var propertyName = property.Name.ToLower(culture);

                    var val = _konfiggy.GetConnectionString(propertyName);

                    if (!string.IsNullOrEmpty(val))
                    {
                        property.SetValue(_config, Convert.ChangeType(val, property.PropertyType));
                    }
                }
                catch (KonfiggyKeyNotFoundException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    throw new KonfiggyUnmappableConfigValueException("Could not map a key/value from appSettings", ex);
                }
            }

            return this;

            /*
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
            */
        }
    }
}
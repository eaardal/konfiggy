using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KonfiggyFramework.Exceptions;
using KonfiggyFramework.Settings;

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

        public T Populate()
        {
            return _config;
        }

        public ConfigurationLoader<T> WithAppSettings(Func<ConfigurationBuilder<T>, ConfigurationBuilder<T>> appSettingToConfigMap)
        {
            var builder = new ConfigurationBuilder<T>();

            var map = appSettingToConfigMap(builder).Build();

            return WithAppSettings(map);
        }

        public ConfigurationLoader<T> WithAppSettings()
        {
            return WithAppSettings(new Dictionary<string, string>());
        }

        public ConfigurationLoader<T> WithConnectionStrings(Func<ConfigurationBuilder<T>, ConfigurationBuilder<T>> connStringToConfigMap)
        {
            var builder = new ConfigurationBuilder<T>();

            var map = connStringToConfigMap(builder).Build();

            return WithConnectionStrings(map);
        }

        public ConfigurationLoader<T> WithConnectionStrings()
        {
            return WithConnectionStrings(new Dictionary<string, string>());
        }

        private ConfigurationLoader<T> WithConnectionStrings(IReadOnlyDictionary<string, string> configurationMap)
        {
            var properties = typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite);

            foreach (var property in properties)
            {
                try
                {
                    var value = GetConnectionStringValue(configurationMap, property);

                    if (string.IsNullOrEmpty(value)) continue;

                    SetValueForProperty(property, value);
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

        private ConfigurationLoader<T> WithAppSettings(IReadOnlyDictionary<string, string> configurationMap)
        {
            var properties = typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite);

            foreach (var property in properties)
            {
                try
                {
                    var value = GetAppSettingValue(configurationMap, property);

                    if (string.IsNullOrEmpty(value)) continue;

                    SetValueForProperty(property, value);
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

        private void SetValueForProperty(PropertyInfo property, string value)
        {
            if (property.PropertyType == typeof(IEnumerable<string>) && value.Contains(';'))
            {
                property.SetValue(_config, value.Split(';'));
            }
            else
            {
                property.SetValue(_config, ConvertValue(value, property));
            }
        }

        private static object ConvertValue(string value, PropertyInfo property)
        {
            return Convert.ChangeType(value, property.PropertyType, KonfiggySettings.Culture);
        }

        private string GetAppSettingValue(IReadOnlyDictionary<string, string> configurationMap, PropertyInfo property)
        {
            var key = GetKey(configurationMap, property);

            return _konfiggy.GetAppSetting(key);
        }

        private string GetConnectionStringValue(IReadOnlyDictionary<string, string> configurationMap, PropertyInfo property)
        {
            var key = GetKey(configurationMap, property);

            return _konfiggy.GetConnectionString(key);
        }

        private static string GetKey(IReadOnlyDictionary<string, string> configurationMap, PropertyInfo property)
        {
            var propertyName = property.Name.ToLower(KonfiggySettings.Culture);

            return configurationMap.ContainsKey(propertyName)
                ? configurationMap.Single(x => x.Key == propertyName).Value
                : propertyName;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KonfiggyFramework.Exceptions;
using KonfiggyFramework.Helpers;
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
            if (IsIEnumerable(property))
            {
                Type innerType;
                var hasSingleTypeParam = TryGetIEnumerableType(property, out innerType);

                if (!hasSingleTypeParam) return;

                var values = value.Split(KonfiggySettings.ConfigListValueSeparator);

                var list = values.Select(v => ConvertValue(v, Type.GetTypeCode(innerType))).CastGeneric(innerType);

                property.SetValue(_config, list);
            }

            #region TODO: Dictionary parsing

            //else if (IsKeyValueCollection(property))
            //{
            //    Type keyType;
            //    Type valueType;
            //    var foundDictTypes = TryGetDictionaryTypes(property, out keyType, out valueType);

            //    if (!foundDictTypes) return;

            //    var kvps = value.Split(KonfiggySettings.ConfigListValueSeparator).Select(x => x.Split(KonfiggySettings.ConfigDictionarySeparator));

            //    var list = kvps.Select(v => ConvertValue(v, Type.GetTypeCode(innerType))).CastGeneric(innerType);

            //    //property.SetValue(_config, list);
            //}

            #endregion

            else
            {
                property.SetValue(_config, ConvertValue(value, property));
            }
        }

        private bool IsKeyValueCollection(PropertyInfo property)
        {
            return property.PropertyType.IsGenericType && property.PropertyType.FullName.Contains("Dictionary");
        }

        private bool IsIEnumerable(PropertyInfo property)
        {
            return property.PropertyType.IsGenericType && property.PropertyType.FullName.Contains("IEnumerable");
        }

        private bool TryGetDictionaryTypes(PropertyInfo property, out Type keyType, out Type valueType)
        {
            var genericTypeArguments = property.PropertyType.GenericTypeArguments;

            if (genericTypeArguments.Count() == 2)
            {
                keyType = genericTypeArguments.First();
                valueType = genericTypeArguments.Last();
                return true;
            }

            keyType = null;
            valueType = null;
            return false;
        }

        private bool TryGetIEnumerableType(PropertyInfo property, out Type type)
        {
            var genericTypeArguments = property.PropertyType.GenericTypeArguments;

            if (genericTypeArguments.Count() == 1)
            {
                type = genericTypeArguments.Single();
                return true;
            }

            type = null;
            return false;
        }

        private static object ConvertValue(string value, TypeCode type)
        {
            return Convert.ChangeType(value, type, KonfiggySettings.Culture);
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
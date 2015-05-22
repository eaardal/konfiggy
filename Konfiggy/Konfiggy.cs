using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using KonfiggyFramework.Exceptions;
using KonfiggyFramework.KeyValueRetrievalStrategies;
using KonfiggyFramework.TagStrategies;

namespace KonfiggyFramework
{
    using System.Dynamic;

    using KonfiggyFramework.Helpers;

    /// <summary>
    /// The main entry point for using Konfiggy.
    /// </summary>
    public class Konfiggy : IKonfiggy
    {
        /// <summary>
        /// The <see cref="IEnvironmentTagStrategy"/> describes how Konfiggy will look for and retrieve the Konfiggy Environment Tag.
        /// The Environment Tag indicates the current environment such as "Dev", "QA", "Prod" etc. Its value will be used when retrieving
        /// an entry from app/web.config. If you ask for the app setting "MyFile" for example, and the <see cref="IEnvironmentTagStrategy"/> 
        /// resolves the Environment Tag to be "Prod", Konfiggy will look for an app setting entry called "Prod.MyFile".
        /// </summary>
        public IEnvironmentTagStrategy EnvironmentTagStrategy { get; set; }

        /// <summary>
        /// The <see cref="IConfigurationKeeper"/> is a wrapper around the mechanic to get the actual data containing the 
        /// environment-aware settings and configurations.
        /// By default this is using <see cref="System.Configuration.ConfigurationManager"/> to retrieve configuration info
        /// </summary>
        public IConfigurationKeeper ConfigurationKeeper { get; set; }

        /// <summary>
        /// The strategy for retrieving the key-value collection that contains the configuration values.
        /// </summary>
        private IKeyValueRetrievalStrategy _keyValueRetrievalStrategy;

        /// <summary>
        /// Constructs Konffigy, using <see cref="KonfiggyFramework.ConfigurationKeeper"/> as the default <see cref="ConfigurationKeeper"/> 
        /// and <see cref="ConfigFileTagStrategy"/> as the default <see cref="EnvironmentTagStrategy"/>
        /// </summary>
        public Konfiggy()
        {
            ConfigurationKeeper = new ConfigurationKeeper();
            EnvironmentTagStrategy = new ConfigFileTagStrategy();
        }

        /// <summary>
        /// Get a connection string by its name. By default this looks in the app/web.config's connectionStrings section.
        /// </summary>
        /// <param name="name">The name of the connection string to look for</param>
        /// <returns>Returns the connection string found matching the name and the current Environment Tag</returns>
        public string GetConnectionString(string name)
        {
            _keyValueRetrievalStrategy = new ConnectionStringsRetrievalStrategy();
            return GetValue(name);
        }

        /// <summary>
        /// Get an <see cref="T:System.Collections.IDictionary"/>{TKey, TValue} of all connection strings.
        ///  By default this looks in the app/web.config's connectionStrings section.
        /// </summary>
        /// <returns>Returns all the settings in the app's app/web.config connectionStrings section</returns>
        public IDictionary<string, string> GetConnectionStrings()
        {
            _keyValueRetrievalStrategy = new ConnectionStringsRetrievalStrategy();

            return _keyValueRetrievalStrategy.GetKeyValueCollection(ConfigurationKeeper);
        }

        /// <summary>
        /// Get an <see cref="T:System.Dynamic.ExpandoObject"/> whose properties
        /// are the connectionstrings in the connectionStrings section of the
        /// app/web.config. By default this looks in the app/web.config's
        /// connectionStrings section.
        /// </summary>
        /// <returns>Returns all the connectionstrings in the app's
        /// app/web.config connectionStrings section as properties on an 
        /// <see cref="T:System.Dynamic.ExpandoObject"/></returns>
        public ExpandoObject GetConnectionStringsDynamic()
        {
            _keyValueRetrievalStrategy = new ConnectionStringsRetrievalStrategy();

            return _keyValueRetrievalStrategy.GetKeyValueCollection(ConfigurationKeeper).ToExpando();
        }

        /// <summary>
        /// Get an app setting entry by its key. By default this looks in the app/web.config's appSettings section.
        /// </summary>
        /// <param name="key">The key of the key-value entry to look for.</param>
        /// <returns>Returns the value of the key-value entry matching the key given and the current Environment Tag</returns>
        public string GetAppSetting(string key)
        {
            _keyValueRetrievalStrategy = new AppSettingsRetrievalStrategy();
            return GetValue(key);
        }

        /// <summary>
        /// Get an <see cref="T:System.Collections.IDictionary"/>{TKey, TValue} of all app settings.
        ///  By default this looks in the app/web.config's appSettings section.
        /// </summary>
        /// <returns>Returns all the settings in the app's app/web.config appSettings section</returns>
        public IDictionary<string, string> GetAppSettings()
        {
            _keyValueRetrievalStrategy = new AppSettingsRetrievalStrategy();

            return _keyValueRetrievalStrategy.GetKeyValueCollection(ConfigurationKeeper);
        }

        /// <summary>
        /// Get an <see cref="T:System.Dynamic.ExpandoObject"/> whose properties
        /// are the settings in the appSettings section of the app/web.config.
        /// By default this looks in the app/web.config's appSettings section.
        /// </summary>
        /// <returns>Returns all the settings in the app's app/web.config
        /// appSettings section as properties on an 
        /// <see cref="T:System.Dynamic.ExpandoObject"/></returns>
        public ExpandoObject GetAppSettingsDynamic()
        {
            _keyValueRetrievalStrategy = new AppSettingsRetrievalStrategy();

            return _keyValueRetrievalStrategy.GetKeyValueCollection(ConfigurationKeeper).ToExpando();
        }

        /// <summary>
        /// Get the value in a key-value collection matching the key given. Please provide the functionality for retrieving the key-value collection.  
        /// </summary>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="keyValueRetrievalStrategy">The custom implementation to use for retrieving the key-value collection. 
        /// The current IConfigurationKeeper instance will be given as input parameter. </param>
        /// <returns>Returns the value of the key-value entry matching the key given and the current Environment Tag.</returns>
        public string GetCustom(string key, IKeyValueRetrievalStrategy keyValueRetrievalStrategy)
        {
            _keyValueRetrievalStrategy = keyValueRetrievalStrategy;
            return GetValue(key);
        }

        public IConfigurationLoader<T> PopulateConfig<T>() where T : new()
        {
            return new ConfigurationLoader<T>(this);
        }

        private string GetValue(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            VerifyConfigurations();

            var dictionary = _keyValueRetrievalStrategy.GetKeyValueCollection(ConfigurationKeeper);

            var environmentTag = EnvironmentTagStrategy.GetEnvironmentTag();

            if (String.IsNullOrEmpty(environmentTag))
                throw new KonfiggyEnvironmentTagNotFoundException(
                    "Could not find any Konfiggy environment tag with the IEnvironmentTagStrategy: " + EnvironmentTagStrategy.GetType().FullName);

            var completeKey = CreateCompleteKey(environmentTag, key);
            return GetValueForKeyInCollection(completeKey, dictionary);
        }

        private void VerifyConfigurations()
        {
            if (EnvironmentTagStrategy == null)
                throw new KonfiggyTagStrategyNotSetException("Please provider an implementation of IEnvironmentTagStrategy");

            if (ConfigurationKeeper == null)
                throw new KonfiggyConfigurationKeeperNotSetException("Please provide an implementation of IConfigurationKeeper");
        }

        private string CreateCompleteKey(string environmentTag, string key)
        {
            return String.Format("{0}.{1}", environmentTag, key);
        }

        private string GetValueForKeyInCollection(string fullKey, IDictionary<string, string> collection)
        {
            var culture = CultureInfo.InvariantCulture;

            try
            {
                var keyLowerCase = fullKey.ToLower(culture);

                string value;

                if (collection.Keys.Select(k => k.ToLower(culture)).Contains(keyLowerCase))
                {
                    value = collection.Single(kvp => kvp.Key.ToLower(culture) == keyLowerCase).Value;
                }
                else
                {
                    var key = fullKey.Remove(0, fullKey.IndexOf('.') + 1);
                    value = collection[key];
                }

                return value;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KonfiggyKeyNotFoundException("Could not find any configuration entry in the name-value collection with the key " + fullKey, ex);
            }
        }
    }
}
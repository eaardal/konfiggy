using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using Konfiggy.Exceptions;
using Konfiggy.Helpers;
using Konfiggy.TagStrategies;

namespace Konfiggy
{
    /// <summary>
    /// The main entry point for using Konfiggy.
    /// </summary>
    public static class Konfiggy
    {
        /// <summary>
        /// The <see cref="IEnvironmentTagStrategy"/> describes how Konfiggy will look for and retrieve the Konfiggy Environment Tag.
        /// The Environment Tag indicates the current environment such as "Dev", "QA", "Prod" etc. Its value will be used when retrieving
        /// an entry from app/web.config. If you ask for the app setting "MyFile" for example, and the <see cref="IEnvironmentTagStrategy"/> 
        /// resolves the Environment Tag to be "Prod", Konfiggy will look for an app setting entry called "Prod.MyFile".
        /// </summary>
        public static IEnvironmentTagStrategy EnvironmentTagStrategy { get; set; }

        /// <summary>
        /// The <see cref="IConfigurationKeeper"/> is a wrapper around the mechanic to get the actual data containing the 
        /// environment-aware settings and configurations.
        /// By default this is using <see cref="System.Configuration.ConfigurationManager"/> 
        /// </summary>
        public static IConfigurationKeeper ConfigurationKeeper { get; set; }

        static Konfiggy()
        {
            ConfigurationKeeper = new ConfigurationKeeper();
            EnvironmentTagStrategy = new ConfigFileGlobalVariableTagStrategy();
        }

        public static string GetConnectionString(string name)
        {
            var kvpRetrieval = new Func<IDictionary<string, string>>(() =>
            {
                var configSection = (ConnectionStringsSection) ConfigurationKeeper.GetSection("connectionStrings");
                var connectionStrings = configSection.ConnectionStrings;
                return ConvertToDictionary(connectionStrings);
            });

            return GetValue(name, kvpRetrieval);
        }

        public static string GetAppSetting(string key)
        {
            var kvpRetrieval = new Func<IDictionary<string, string>>(() =>
            {
                var appSettings = (NameValueCollection) ConfigurationKeeper.GetSection("appSettings");
                return ConvertToDictionary(appSettings);
            });

            return GetValue(key, kvpRetrieval);
        }

        private static string GetValue(string key, Func<IDictionary<string, string>> getKeyValueCollection)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            VerifyConfigurations();

            var dictionary = getKeyValueCollection();

            var environmentTag = EnvironmentTagStrategy.GetEnvironmentTag();

            if (String.IsNullOrEmpty(environmentTag))
                throw new KonfiggyEnvironmentTagNotFoundException(
                    "Could not find any Konfiggy environment tag with the IEnvironmentTagStrategy given");

            var completeKey = CreateCompleteKey(environmentTag, key);
            return GetValueForKeyInCollection(completeKey, dictionary);
        }

        private static IDictionary<string, string> ConvertToDictionary(ConnectionStringSettingsCollection connectionStrings)
        {
            var convertAction = new Action<IDictionary<string, string>>(dictionary =>
            {
                for (int i = 0; i < connectionStrings.Count; i++)
                    dictionary.Add(connectionStrings[i].Name, connectionStrings[i].ConnectionString);
            });
            return ConvertToDictionary(convertAction);
        } 

        private static IDictionary<string, string> ConvertToDictionary(NameValueCollection nameValueCollection)
        {
            var convertAction = new Action<IDictionary<string, string>>(dictionary =>
            {
                for (int i = 0; i < nameValueCollection.Count; i++)
                    dictionary.Add(nameValueCollection.GetKey(i), nameValueCollection[i]);
            });
            return ConvertToDictionary(convertAction);
        }

        private static IDictionary<string, string> ConvertToDictionary(Action<IDictionary<string, string>> fillDictionaryAction)
        {
            var dictionary = new Dictionary<string, string>();
            fillDictionaryAction(dictionary);
            return dictionary;
        } 

        private static void VerifyConfigurations()
        {
            if (EnvironmentTagStrategy == null)
                throw new KonfiggyTagStrategyNotSetException("Please provider an implementation of IEnvironmentTagStrategy through calling the Konfiggy.Initialize() method");

            if (ConfigurationKeeper == null)
                throw new KonfiggyConfigurationKeeperNotSetException("Please provide an implementation of IConfiguraitonKeeper through calling the Konfiggy.Initialize() method");
        }

        private static string CreateCompleteKey(string environmentTag, string key)
        {
            return String.Format("{0}.{1}", environmentTag, key);
        }

        private static string GetValueForKeyInCollection(string fullKey, IDictionary<string, string> collection)
        {
            string value = collection[fullKey];
            if (String.IsNullOrEmpty(value))
                throw new KonfiggyKeyNotFoundException(
                    "Could not find any configuration entry in the name-value collection with the key " + fullKey);

            return value;
        }
    }
}
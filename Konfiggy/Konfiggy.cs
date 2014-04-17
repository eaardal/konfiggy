using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Konfiggy.Exceptions;
using Konfiggy.Helpers;
using Konfiggy.TagStrategies;

namespace Konfiggy
{
    public static class Konfiggy
    {
        public static IEnvironmentTagStrategy EnvironmentTagStrategy { get; set; }

        public static IConfigurationKeeper ConfigurationKeeper { get; set; }

        static Konfiggy()
        {
            ConfigurationKeeper = new ConfigurationKeeper();
            EnvironmentTagStrategy = new ConfigFileGlobalVariableTagStrategy();
        }

        public static void Initialize(IEnvironmentTagStrategy environmentTagStrategy, IConfigurationKeeper configurationKeeper)
        {
            if (environmentTagStrategy == null) throw new ArgumentNullException("environmentTagStrategy");
            if (configurationKeeper == null) throw new ArgumentNullException("configurationKeeper");
            EnvironmentTagStrategy = environmentTagStrategy;
            ConfigurationKeeper = configurationKeeper;
        }

        public static string GetAppSetting(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (EnvironmentTagStrategy == null)
                throw new KonfiggyNoTagStrategiesSetException("Please provider an implementation of IEnvironmentTagStrategy through calling the Konfiggy.Initialize() method");

            if (ConfigurationKeeper == null)
                throw new KonfiggyConfigurationKeeperNotSetException("Please provide an implementation of IConfiguraitonKeeper through calling the Konfiggy.Initialize() method");

            var appSettings = (NameValueCollection) ConfigurationKeeper.GetSection("appSettings");

            var environmentTag = EnvironmentTagStrategy.GetEnvironmentTag();

            if (String.IsNullOrEmpty(environmentTag))
                throw new KonfiggyEnvironmentTagNotFoundException(
                    "Could not find any Konfiggy environment tag with the IEnvironmentTagStrategy given");

            var completeKey = CreateCompleteKey(environmentTag, key);
            return GetValueForKeyInCollection(completeKey, appSettings);
        }

        private static string CreateCompleteKey(string environmentTag, string key)
        {
            return String.Format("{0}.{1}", environmentTag, key);
        }

        private static string GetValueForKeyInCollection(string fullKey, NameValueCollection collection)
        {
            string value = collection[fullKey];
            if (String.IsNullOrEmpty(value))
                throw new KonfiggyNoConfigurationKeyFoundException(
                    "Could not find any configuration entry in the name-value collection with the key " + fullKey);

            return value;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Konfiggy.Exceptions;
using Konfiggy.TagStrategies;

namespace Konfiggy
{
    public static class Konfiggy
    {
        private static List<IKonfiggyTagStrategy> _konfiggyTagStrategies; 

        static Konfiggy()
        {
            _konfiggyTagStrategies = new List<IKonfiggyTagStrategy>();
        }

        public static void Initialize(params IKonfiggyTagStrategy[] konfiggyTagStrategies)
        {
            if (konfiggyTagStrategies == null) throw new ArgumentNullException("konfiggyTagStrategies");
            _konfiggyTagStrategies = konfiggyTagStrategies.ToList();
        }

        public static string GetAppSetting(string key)
        {
            var appSettings = (NameValueCollection) ConfigurationManager.GetSection("appSettings");

            string environmentTag =
                _konfiggyTagStrategies.Select(strat => strat.GetEnvironmentTag()).FirstOrDefault(result => !String.IsNullOrEmpty(result));

            if (String.IsNullOrEmpty(environmentTag))
                throw new KonfiggyEnvironmentTagNotFoundException("Could not find any Konfiggy environment tags for any of the strategies given.");

            string fullKey = String.Format("{0}.{1}", environmentTag, key);
            string value = appSettings[fullKey];
            if (String.IsNullOrEmpty(value))
            {
                throw new KonfiggyNoConfigurationKeyFoundException("Could not find any configuration entry in the appSettings section with the key " + fullKey);
            }

            Debug.WriteLine("Full key: {0}, value: {1}", fullKey, value);
            return value;
        }
    }
}
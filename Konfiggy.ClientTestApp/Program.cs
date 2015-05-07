﻿using System;
using System.Collections.Generic;
using KonfiggyFramework;
using KonfiggyFramework.KeyValueRetrievalStrategies;

namespace Konfiggy.ClientTestApp
{
    using KonfiggyFramework.TagStrategies;

    class Program
    {
        static void Main(string[] args)
        {
            IKonfiggy konfiggy = new KonfiggyFramework.Konfiggy();
            konfiggy.ConfigurationKeeper = new ConfigurationKeeper();
            //konfiggy.EnvironmentTagStrategy = new ConfigFileTagStrategy();
            konfiggy.EnvironmentTagStrategy = new CodeTagStrategy("Dev");
            //konfiggy.EnvironmentTagStrategy = new EnvironmentVariableTagStrategy();
            //konfiggy.EnvironmentTagStrategy = new MachineNameTagStrategy(CreateMachineNamesMap());
            //konfiggy.EnvironmentTagStrategy = new TextFileTagStrategy(new DefaultFileSettings());

            var settingValue = konfiggy.GetAppSetting("setting");
            Console.WriteLine("Value for MySetting key: " + settingValue);

            var connStringValue = konfiggy.GetConnectionString("MyConnString");
            Console.WriteLine("Value for connection string MyConnString: " + connStringValue);

            var customStorageValue = konfiggy.GetCustom("MySetting", new CustomKeyValueRetrievalStrategy());
            Console.WriteLine("Value for custom key/value storage: " + customStorageValue);
            
            Console.ReadLine();
        }

        private static IDictionary<string, string> CreateMachineNamesMap()
        {
            return new Dictionary<string, string>
            {
                {"my-computer-name", "Local"},
                {"dev-environment-computer-name", "Dev"},
                {"qa-environment-computer-name", "QA"},
                {"prod-environment-computer-name", "Prod"}
            };
        }
    }

    class CustomKeyValueRetrievalStrategy : IKeyValueRetrievalStrategy
    {
        public IDictionary<string, string> GetKeyValueCollection(IConfigurationKeeper configurationKeeper)
        {
            return new Dictionary<string, string>
            {
                {"Local.MySetting", "Local-environment-specific-value-from-custom-storage"},
                {"Dev.MySetting", "Dev-environment-specific-value-from-custom-storage"},
                {"QA.MySetting", "QA-environment-specific-value-from-custom-storage"},
                {"Prod.MySetting", "Prod-environment-specific-value-from-custom-storage"},
            };
        }
    }
}

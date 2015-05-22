using System;
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
            var config = konfiggy.PopulateConfig<AppConfig>().WithAppSettings().WithConnectionStrings().Populate();
            var config2 = konfiggy.PopulateConfig<AppConfig>()
                .WithAppSettings(c => c.Map(x => x.Setting, "Setting").Map(x => x.MySetting, "MySettingasd"))
                .WithConnectionStrings(c => c.Map(x => x.MyConnString, "MyConnString"))
                .Populate();

            Console.WriteLine(config2.Setting);
            Console.WriteLine(config2.MySetting);
            Console.WriteLine(config2.MyConnString);


            //Console.WriteLine("Value for appSetting MySetting: " + config.MySetting);
            //Console.WriteLine("Value for appSetting Setting: " + config.Setting);
            //Console.WriteLine("Value for connString MyConnString: " + config.MyConnString);

            konfiggy.ConfigurationKeeper = new ConfigurationKeeper();
            //konfiggy.EnvironmentTagStrategy = new ConfigFileTagStrategy();
            konfiggy.EnvironmentTagStrategy = new CodeTagStrategy("Dev");
            //konfiggy.EnvironmentTagStrategy = new EnvironmentVariableTagStrategy();
            //konfiggy.EnvironmentTagStrategy = new MachineNameTagStrategy(CreateMachineNamesMap());
            //konfiggy.EnvironmentTagStrategy = new TextFileTagStrategy(new DefaultFileSettings());

            Console.WriteLine("DEMO: Environment resolution");
            var settingValue = konfiggy.GetAppSetting("setting");
            Console.WriteLine("Value for MySetting key: " + settingValue);
            Console.WriteLine();

            var connStringValue = konfiggy.GetConnectionString("MyConnString");
            Console.WriteLine("Value for connection string MyConnString: " + connStringValue);
            Console.WriteLine();

            var customStorageValue = konfiggy.GetCustom("MySetting", new CustomKeyValueRetrievalStrategy());
            Console.WriteLine("Value for custom key/value storage: " + customStorageValue);
            Console.WriteLine();

            Console.WriteLine("DEMO: Retrieving all settings as dictionary");
            var settings = konfiggy.GetAppSettings();

            foreach (var setting in settings)
            {
                Console.WriteLine("{0} - {1}", setting.Key, setting.Value);
            }
            Console.WriteLine();

            var connections = konfiggy.GetConnectionStrings();

            foreach (var connection in connections)
            {
                Console.WriteLine("{0} - {1}", connection.Key, connection.Value);
            }
            Console.WriteLine();

            Console.WriteLine("DEMO: Retrieving all settings as dynamic");
            dynamic settingsDynamic = konfiggy.GetAppSettingsDynamic();
            Console.WriteLine(settingsDynamic.Setting);
            Console.WriteLine(settingsDynamic.DevMySetting);
            Console.WriteLine();

            dynamic connectionsDynamic = konfiggy.GetConnectionStringsDynamic();
            Console.WriteLine(connectionsDynamic.DevMyConnString);
            Console.WriteLine(connectionsDynamic.QAMyConnString);

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

    internal class AppConfig
    {
        public string MySetting { get; set; }
        public string Setting { get; set; }
        public string MyConnString { get; set; }
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

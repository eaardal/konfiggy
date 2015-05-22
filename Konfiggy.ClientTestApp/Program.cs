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
           
            DemoDynamic(konfiggy);
            Console.WriteLine();

            DemoPopulateConfig(konfiggy);
            Console.WriteLine();

            Console.ReadLine();
        }

        private static void DemoPopulateConfig(IKonfiggy konfiggy)
        {
            Console.WriteLine("DEMO: Populate config");

            var config = konfiggy.PopulateConfig<AppConfig>().WithAppSettings().WithConnectionStrings().Populate();

            Console.WriteLine("setting: " + config.Setting);
            Console.WriteLine("MyConnString: " + config.MyConnString);
            Console.WriteLine("MySetting: " + config.MySetting);
        }

        private static void DemoDynamic(IKonfiggy konfiggy)
        {
            Console.WriteLine("DEMO: Retrieving all settings as dynamic");

            dynamic settingsDynamic = konfiggy.GetAppSettingsDynamic();

            Console.WriteLine(settingsDynamic.Setting);
            Console.WriteLine(settingsDynamic.DevMySetting);

            Console.WriteLine();

            dynamic connectionsDynamic = konfiggy.GetConnectionStringsDynamic();

            Console.WriteLine(connectionsDynamic.DevMyConnString);
            Console.WriteLine(connectionsDynamic.QAMyConnString);
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

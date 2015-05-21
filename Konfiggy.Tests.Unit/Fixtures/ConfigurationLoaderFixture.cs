using System.Collections.Generic;
using KonfiggyFramework;
using KonfiggyFramework.KeyValueRetrievalStrategies;
using Moq;

namespace Konfiggy.Tests.Unit.Fixtures
{
    public class ConfigurationLoaderFixture
    {
        public Mock<IConfigurationKeeper> ConfigurationKeeper { get; set; }
        public Mock<IKeyValueRetrievalStrategy> AppSettingsRetrievalStrategy { get; set; }
        public Mock<IKeyValueRetrievalStrategy> ConnectionStringsRetrievalStrategy { get; set; }

        public ConfigurationLoaderFixture()
        {
            ConfigurationKeeper = new Mock<IConfigurationKeeper>();
            AppSettingsRetrievalStrategy = new Mock<IKeyValueRetrievalStrategy>();
            ConnectionStringsRetrievalStrategy = new Mock<IKeyValueRetrievalStrategy>();
        }

        public ConfigurationLoader<T> CreateSut<T>() where T : new()
        {
            return new ConfigurationLoader<T>(ConfigurationKeeper.Object, AppSettingsRetrievalStrategy.Object, ConnectionStringsRetrievalStrategy.Object);
        }

        public class TestConfig
        {
            public string Key1 { get; set; }
            public string Key2 { get; set; }
            public string Key3 { get; set; }
        }

        public class TestConfig2
        {
            public string key1 { get; set; }
            public string key2 { get; set; }
            public string key3 { get; set; }
        }

        public class TestConfig3
        {
            public string KEY1 { get; set; }
            public string KeY2 { get; set; }
            public string kEy3 { get; set; }
        }

        public class TestConfig4
        {
            public int Key1 { get; set; }
            public double Key2 { get; set; }
            public double Key3 { get; set; }
        }

        public class TestConfig5
        {
            public string Key1 { get; set; }
            public int Key2 { get; set; }
            public bool Key3 { get; set; }
        }

        public class TestConfig6
        {
            public IEnumerable<string> Key1 { get; set; }
        }
    }
}
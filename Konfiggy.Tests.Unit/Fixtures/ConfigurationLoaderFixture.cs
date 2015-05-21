using KonfiggyFramework;
using KonfiggyFramework.KeyValueRetrievalStrategies;
using Moq;

namespace Konfiggy.Tests.Unit.Fixtures
{
    public class ConfigurationLoaderFixture
    {
        public Mock<IConfigurationKeeper> ConfigurationKeeper { get; set; }
        public Mock<IKeyValueRetrievalStrategy> KeyValueRetrievalStrategy { get; set; }

        public ConfigurationLoaderFixture()
        {
            ConfigurationKeeper = new Mock<IConfigurationKeeper>();
            KeyValueRetrievalStrategy = new Mock<IKeyValueRetrievalStrategy>();
        }

        public ConfigurationLoader<TestConfig> CreateSut()
        {
            return new ConfigurationLoader<TestConfig>(ConfigurationKeeper.Object, KeyValueRetrievalStrategy.Object);
        }

        public class TestConfig
        {
            
        }
    }
}
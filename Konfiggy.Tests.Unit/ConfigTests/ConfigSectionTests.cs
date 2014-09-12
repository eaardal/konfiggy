using KonfiggyFramework;
using KonfiggyFramework.Config;
using Moq;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit.ConfigTests
{
    [TestFixture]
    public class ConfigSectionTests
    {
        [Test]
        public void Constructor_SetsConfigurationKeeperToDefault()
        {
            Assert.IsNotNull(ConfigSection.ConfigurationKeeper);
            Assert.IsInstanceOf<ConfigurationKeeper>(ConfigSection.ConfigurationKeeper);
        }

        [Test]
        public void GetConfig_CallsConfigurationKeeperGetConfig()
        {
            var configKeeper = new Mock<IConfigurationKeeper>();
            configKeeper.Setup(ctx => ctx.GetSection("konfiggy")).Verifiable("Failed to call configuration keeper's GetSection method");
            ConfigSection.ConfigurationKeeper = configKeeper.Object;

            ConfigSection.GetConfig();

            configKeeper.Verify();
        }
    }
}

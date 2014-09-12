using KonfiggyFramework;
using KonfiggyFramework.Config;
using KonfiggyFramework.TagStrategies;
using Moq;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit.TagStrategiesTests
{
    [TestFixture]
    public class ConfigFileTagStrategyTests
    {
        private ConfigFileTagStrategy _tagStrategy;

        [SetUp]
        public void SetUp()
        {
            _tagStrategy = new ConfigFileTagStrategy();
        }

        [Test]
        public void GetEnvironmentTag_WhenConfigSectionIsNull_UsesDefault()
        {
            var configSection = new Mock<IConfigSection>();
            configSection.Setup(ctx => ctx.EnvironmentTag.Value).Returns("Local");

            var configKeeper = new Mock<IConfigurationKeeper>();
            configKeeper.Setup(ctx => ctx.GetSection("konfiggy")).Returns(configSection.Object);

            _tagStrategy.ConfigSection = null;
            ConfigSection.ConfigurationKeeper = configKeeper.Object;

            _tagStrategy.GetEnvironmentTag();

            Assert.IsNotNull(_tagStrategy.ConfigSection);
            Assert.IsInstanceOf<IConfigSection>(_tagStrategy.ConfigSection);
            Assert.AreSame(configSection.Object, _tagStrategy.ConfigSection);
        }

        [Test]
        public void GetEnvironmentTag_ReturnsValueOfEnvironmentTagInConfigSection()
        {
            var configSection = new Mock<IConfigSection>();
            configSection.SetupGet(ctx => ctx.EnvironmentTag.Value).Returns("Local");

            _tagStrategy.ConfigSection = configSection.Object;
      
            var value = _tagStrategy.GetEnvironmentTag();

            Assert.AreEqual("Local", value);
        }

       
    }
}

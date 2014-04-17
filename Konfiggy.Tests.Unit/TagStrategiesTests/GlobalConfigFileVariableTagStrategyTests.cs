using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Konfiggy.Config;
using Konfiggy.Exceptions;
using Konfiggy.Helpers;
using Konfiggy.TagStrategies;
using Moq;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit.TagStrategiesTests
{
    [TestFixture]
    public class GlobalConfigFileVariableTagStrategyTests
    {
        private GlobalConfigFileVariableTagStrategy _tagStrategy;

        [SetUp]
        public void SetUp()
        {
            _tagStrategy = new GlobalConfigFileVariableTagStrategy();
        }

        [Test]
        public void GetEnvironmentTag_WhenConfigSectionIsNull_UsesDefault()
        {
            _tagStrategy.ConfigSection = null;

            var configSection = new Mock<IConfigSection>();
            configSection.Setup(ctx => ctx.EnvironmentTag.Value).Returns("Local");

            var configKeeper = new Mock<IConfigurationKeeper>();
            configKeeper.Setup(ctx => ctx.GetSection("konfiggy")).Returns(configSection.Object);

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

        private XDocument GetFakeKonfiggyConfigSection()
        {
            var doc = new XDocument(new XElement("konfiggy",
                new XElement("environmentTag",
                    new XAttribute("value", "Local"))));

            return doc;
        }
    }
}

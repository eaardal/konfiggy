using System;
using System.Collections.Specialized;
using Konfiggy.Exceptions;
using Konfiggy.Helpers;
using Konfiggy.TagStrategies;
using Moq;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit
{
    [TestFixture]
    public class KonfiggyTests
    {
        [Test]
        public void GetAppSettings_WhenNoTagStrategyIsSet_ThrowsException()
        {
            Konfiggy.EnvironmentTagStrategy = null;
            Assert.Throws<KonfiggyTagStrategyNotSetException>(() => Konfiggy.GetAppSetting("somevalue"));
        }

        [Test]
        public void GetAppSettings_WhenNoConfigurationKeeperIsSet_ThrowsException()
        {
            var tagStrat = new Mock<IEnvironmentTagStrategy>();

            Konfiggy.EnvironmentTagStrategy = tagStrat.Object;
            Konfiggy.ConfigurationKeeper = null;

            Assert.Throws<KonfiggyConfigurationKeeperNotSetException>(() => Konfiggy.GetAppSetting("somevalue"));
        }

        [Test]
        public void GeneralBehavior_WhenNoConfigurationKeeperIsGivenThroughInitialize_UsesDefaultConfigurationKeeper()
        {
            Assert.IsNotNull(Konfiggy.ConfigurationKeeper);
            Assert.IsInstanceOf<ConfigurationKeeper>(Konfiggy.ConfigurationKeeper);
        }

        [Test]
        public void GeneralBehavior_WhenNoTagStrategyIsGivenThroughInitialize_UsesDefaultTagStrategy()
        {
            Assert.IsNotNull(Konfiggy.EnvironmentTagStrategy);
            Assert.IsInstanceOf<ConfigFileGlobalVariableTagStrategy>(Konfiggy.EnvironmentTagStrategy);
        }

        [Test]
        public void GetAppSettings_WithTagStrategyThatShouldReturnTagValue_CorrectTagValueIsSet()
        {
            var tagStrat = new Mock<IEnvironmentTagStrategy>();
            tagStrat.Setup(ctx => ctx.GetEnvironmentTag()).Returns("Local");

            var config = new Mock<IConfigurationKeeper>();
            config.Setup(ctx => ctx.GetSection("appSettings")).Returns(GetFakeConfigValues());

            Konfiggy.EnvironmentTagStrategy = tagStrat.Object;
            Konfiggy.ConfigurationKeeper = config.Object;

            string result = Konfiggy.GetAppSetting("TestValue");

            Assert.AreEqual("LocalValue", result);
        }

        [Test]
        public void GetAppSettings_WhenKeyIsNullOrEmpty_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Konfiggy.GetAppSetting(null));
            Assert.Throws<ArgumentNullException>(() => Konfiggy.GetAppSetting(String.Empty));
        }

        private NameValueCollection GetFakeConfigValues()
        {
            return new NameValueCollection
            {
                {"Local.TestValue", "LocalValue"},
                {"Dev.TestValue", "DevValue"},
                {"QA.TestValue", "QAValue"},
                {"Prod.TestValue", "ProdValue"},
            };
        }
    }
}

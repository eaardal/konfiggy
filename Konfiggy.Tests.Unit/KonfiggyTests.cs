using System;
using System.Collections.Specialized;
using System.Configuration;
using KonfiggyFramework;
using KonfiggyFramework.Exceptions;
using KonfiggyFramework.TagStrategies;
using Moq;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit
{
    [TestFixture]
    public class KonfiggyTests
    {
        private IKonfiggy _konfiggy;

        [SetUp]
        public void SetUp()
        {
            _konfiggy = new KonfiggyFramework.Konfiggy();
        }

        [Test]
        public void GetAppSettings_WhenNoTagStrategyIsSet_ThrowsException()
        {
            _konfiggy.EnvironmentTagStrategy = null;
            Assert.Throws<KonfiggyTagStrategyNotSetException>(() => _konfiggy.GetAppSetting("somevalue"));
        }

        [Test]
        public void GetAppSettings_WhenNoConfigurationKeeperIsSet_ThrowsException()
        {
            var tagStrat = new Mock<IEnvironmentTagStrategy>();

            _konfiggy.EnvironmentTagStrategy = tagStrat.Object;
            _konfiggy.ConfigurationKeeper = null;

            Assert.Throws<KonfiggyConfigurationKeeperNotSetException>(() => _konfiggy.GetAppSetting("somevalue"));
        }

        [Test]
        public void Constructor_SetsDefaultConfigurationKeeper()
        {
            Assert.IsNotNull(_konfiggy.ConfigurationKeeper);
            Assert.IsInstanceOf<ConfigurationKeeper>(_konfiggy.ConfigurationKeeper);
        }

        [Test]
        public void Constructor_SetsDefaultEnvironmentTagStrategy()
        {
            Assert.IsNotNull(_konfiggy.EnvironmentTagStrategy);
            Assert.IsInstanceOf<ConfigFileTagStrategy>(_konfiggy.EnvironmentTagStrategy);
        }

        [Test]
        public void GetAppSettings_WithTagStrategyThatShouldReturnTagValue_CorrectTagValueIsSet()
        {
            var tagStrat = new Mock<IEnvironmentTagStrategy>();
            tagStrat.Setup(ctx => ctx.GetEnvironmentTag()).Returns("Local");

            var config = new Mock<IConfigurationKeeper>();
            config.Setup(ctx => ctx.GetSection("appSettings")).Returns(GetFakeAppSettings());

            _konfiggy.EnvironmentTagStrategy = tagStrat.Object;
            _konfiggy.ConfigurationKeeper = config.Object;

            string result = _konfiggy.GetAppSetting("TestValue");

            Assert.AreEqual("LocalValue", result);
        }

        [Test]
        public void GetAppSettings_WithNoEnvironmentTagInKeyShouldReturnCommonValue_TagValueIsIgnored()
        {
            var tagStrat = new Mock<IEnvironmentTagStrategy>();
            tagStrat.Setup(ctx => ctx.GetEnvironmentTag()).Returns("Local");

            var config = new Mock<IConfigurationKeeper>();
            config.Setup(ctx => ctx.GetSection("appSettings")).Returns(GetFakeAppSettings());

            _konfiggy.EnvironmentTagStrategy = tagStrat.Object;
            _konfiggy.ConfigurationKeeper = config.Object;

            string result = _konfiggy.GetAppSetting("CommonTestValue");

            Assert.AreEqual("CommonValue", result);
        }

        [Test]
        public void WhenKeyDoesNotExistInKeyValueStore_ThrowsKonfiggyKeyNotFoundException()
        {
            var tagStrat = new Mock<IEnvironmentTagStrategy>();
            tagStrat.Setup(ctx => ctx.GetEnvironmentTag()).Returns("Local");

            var config = new Mock<IConfigurationKeeper>();
            config.Setup(ctx => ctx.GetSection("connectionStrings")).Returns(GetFakeConnectionStrings);

            _konfiggy.EnvironmentTagStrategy = tagStrat.Object;
            _konfiggy.ConfigurationKeeper = config.Object;
            
            Assert.Throws<KonfiggyKeyNotFoundException>(() => _konfiggy.GetConnectionString("NonExistingKey"));
        }

        [Test]
        public void GetAppSettings_WhenKeyIsNullOrEmpty_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _konfiggy.GetAppSetting(null));
            Assert.Throws<ArgumentNullException>(() => _konfiggy.GetAppSetting(String.Empty));
        }

        private ConnectionStringsSection GetFakeConnectionStrings()
        {
            var section = new ConnectionStringsSection();
            section.ConnectionStrings.Add(new ConnectionStringSettings("Local.TestKey", "LocalConnectionStringValue"));
            section.ConnectionStrings.Add(new ConnectionStringSettings("Dev.TestKey", "DevConnectionStringValue"));
            section.ConnectionStrings.Add(new ConnectionStringSettings("QA.TestKey", "QAConnectionStringValue"));
            section.ConnectionStrings.Add(new ConnectionStringSettings("Prod.TestKey", "ProdConnectionStringValue"));
            return section;
        }

        private NameValueCollection GetFakeAppSettings()
        {
            return new NameValueCollection
            {
                {"CommonTestValue", "CommonValue"},
                {"Local.TestValue", "LocalValue"},
                {"Dev.TestValue", "DevValue"},
                {"QA.TestValue", "QAValue"},
                {"Prod.TestValue", "ProdValue"},
            };
        }
    }
}

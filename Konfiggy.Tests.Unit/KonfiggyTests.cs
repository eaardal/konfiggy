using System;
using System.Linq;
using System.Linq.Expressions; 
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
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

        #region Default/Constructor

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

        #endregion

        #region GetAppSettings Method

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
        public void GetAppSettings_WhenKeyIsNullOrEmpty_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _konfiggy.GetAppSetting(null));
            Assert.Throws<ArgumentNullException>(() => _konfiggy.GetAppSetting(String.Empty));
        }

        [Test]
        public void GetAppSettings_WhenRequestedKeyDoesNotMatchCase()
        {
            var tagStrat = new Mock<IEnvironmentTagStrategy>();
            tagStrat.Setup(ctx => ctx.GetEnvironmentTag()).Returns("Local");

            var config = new Mock<IConfigurationKeeper>();
            config.Setup(ctx => ctx.GetSection("appSettings")).Returns(GetFakeAppSettings());

            _konfiggy.EnvironmentTagStrategy = tagStrat.Object;
            _konfiggy.ConfigurationKeeper = config.Object;

            string result = _konfiggy.GetAppSetting("tEstVALue");

            Assert.AreEqual("LocalValue", result);
        }

        #endregion

        #region GetAppSettingsDynamic Method

        [Test]
        public void GetAppSettingsDynamic_ReturnsEnvironmentSpecificValue()
        {
            var tagStrat = new Mock<IEnvironmentTagStrategy>();
            tagStrat.Setup(ctx => ctx.GetEnvironmentTag()).Returns("Local");

            var config = new Mock<IConfigurationKeeper>();
            var appSettings = GetFakeAppSettings();
            config.Setup(ctx => ctx.GetSection("appSettings")).Returns(appSettings);

            _konfiggy.EnvironmentTagStrategy = tagStrat.Object;
            _konfiggy.ConfigurationKeeper = config.Object;

            dynamic result = _konfiggy.GetAppSettingsDynamic();

            Assert.NotNull(result.CommonTestValue);
            Assert.AreEqual(appSettings["CommonTestValue"], result.CommonTestValue);

            Assert.NotNull(result.LocalTestValue);
            Assert.AreEqual(appSettings["Local.TestValue"], result.LocalTestValue);

            Assert.NotNull(result.DevTestValue);
            Assert.AreEqual(appSettings["Dev.TestValue"], result.DevTestValue);

            Assert.NotNull(result.QATestValue);
            Assert.AreEqual(appSettings["QA.TestValue"], result.QATestValue);

            Assert.NotNull(result.ProdTestValue);
            Assert.AreEqual(appSettings["Prod.TestValue"], result.ProdTestValue);
        }

        #endregion

        #region GetConnectionString Method

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

        #endregion

        #region GetConnectionStringsDynamic Method

        [Test]
        public void GetConnectionStringsDynamic_ReturnsEnvironmentSpecificValue()
        {
            var tagStrat = new Mock<IEnvironmentTagStrategy>();
            tagStrat.Setup(ctx => ctx.GetEnvironmentTag()).Returns("Local");

            var config = new Mock<IConfigurationKeeper>();
            var connStringSection = GetFakeConnectionStrings();
            var connectionStrings = connStringSection.ConnectionStrings;
            config.Setup(ctx => ctx.GetSection("connectionStrings")).Returns(connStringSection);

            _konfiggy.EnvironmentTagStrategy = tagStrat.Object;
            _konfiggy.ConfigurationKeeper = config.Object;

            dynamic result = _konfiggy.GetConnectionStringsDynamic();

            Assert.NotNull(result.CommonTestKey);
            Assert.AreEqual(connectionStrings["CommonTestKey"].ConnectionString, result.CommonTestKey);

            Assert.NotNull(result.LocalTestKey);
            Assert.AreEqual(connectionStrings["Local.TestKey"].ConnectionString, result.LocalTestKey);

            Assert.NotNull(result.DevTestKey);
            Assert.AreEqual(connectionStrings["Dev.TestKey"].ConnectionString, result.DevTestKey);

            Assert.NotNull(result.QATestKey);
            Assert.AreEqual(connectionStrings["QA.TestKey"].ConnectionString, result.QATestKey);

            Assert.NotNull(result.ProdTestKey);
            Assert.AreEqual(connectionStrings["Prod.TestKey"].ConnectionString, result.ProdTestKey);
        }

        #endregion

        private ConnectionStringsSection GetFakeConnectionStrings()
        {
            var section = new ConnectionStringsSection();
            section.ConnectionStrings.Add(new ConnectionStringSettings("CommonTestKey", "CommonConnectionStringValue"));
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

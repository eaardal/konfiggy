using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Konfiggy.Tests.Unit.Fixtures;
using KonfiggyFramework;
using Moq;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit
{
    [TestFixture]
    public class ConfigurationLoaderTests
    {
        private ConfigurationLoaderFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new ConfigurationLoaderFixture();
        }

        #region Smoke Tests

        [Test]
        public void CreatesSystemUnderTest()
        {
            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig>();
            Assert.NotNull(sut);
            Assert.IsInstanceOf<ConfigurationLoader<ConfigurationLoaderFixture.TestConfig>>(sut);
        }

        #endregion

        #region Populate Method

        [Test]
        public void Load_ReturnsInstanceOfProvidedType()
        {
            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig>();

            var config = sut.Populate();

            Assert.NotNull(config);
            Assert.IsInstanceOf<ConfigurationLoaderFixture.TestConfig>(config);
        }

        #endregion

        #region WithAppSettings (Parameterless overload) Method

        [Test]
        public void WithAppSettings_CallsGetAppSettingsOnKonfiggy()
        {
            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig>();

            sut.WithAppSettings();
            
            _fixture.Konfiggy.Verify(x => x.GetAppSetting(It.IsAny<string>()));
        }

        [Test]
        public void WithAppSettings_WhenPropertiesArePascalCase_AndAppSettingKeysMatchPropertyCase_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"Key1", "Value1"},
                {"Key2", "Value2"},
                {"Key3", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.Key1);
            Assert.AreEqual(dict.ElementAt(1).Value, config.Key2);
            Assert.AreEqual(dict.ElementAt(2).Value, config.Key3);
        }

        [Test]
        public void WithAppSettings_WhenPropertiesArePascalCase_AndAppSettingKeysAreLowerCase_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"key1", "Value1"},
                {"key2", "Value2"},
                {"key3", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.Key1);
            Assert.AreEqual(dict.ElementAt(1).Value, config.Key2);
            Assert.AreEqual(dict.ElementAt(2).Value, config.Key3);
        }

        [Test]
        public void WithAppSettings_WhenPropertiesArePascalCase_AndAppSettingKeysAreInvariantCase_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"kEy1", "Value1"},
                {"KeY2", "Value2"},
                {"KEY3", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.Key1);
            Assert.AreEqual(dict.ElementAt(1).Value, config.Key2);
            Assert.AreEqual(dict.ElementAt(2).Value, config.Key3);
        }

        [Test]
        public void WithAppSettings_WhenPropertiesAreLowerCase_AndAppSettingKeysAreLowerCase_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"key1", "Value1"},
                {"key2", "Value2"},
                {"key3", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig2>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.key1);
            Assert.AreEqual(dict.ElementAt(1).Value, config.key2);
            Assert.AreEqual(dict.ElementAt(2).Value, config.key3);
        }

        [Test]
        public void WithAppSettings_WhenPropertiesAreLowerCase_AndAppSettingKeysAreInvariantCase_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"kEy1", "Value1"},
                {"KeY2", "Value2"},
                {"KEY3", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig2>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.key1);
            Assert.AreEqual(dict.ElementAt(1).Value, config.key2);
            Assert.AreEqual(dict.ElementAt(2).Value, config.key3);
        }

        [Test]
        public void WithAppSettings_WhenPropertiesAreInvariantCase_AndAppSettingKeysAreInvariantCase_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"kEy1", "Value1"},
                {"KeY2", "Value2"},
                {"KEY3", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig3>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.KEY1);
            Assert.AreEqual(dict.ElementAt(1).Value, config.KeY2);
            Assert.AreEqual(dict.ElementAt(2).Value, config.kEy3);
        }

        [Test]
        public void WithAppSettings_WhenPropertiesAreIntAndDouble_AndAppSettingValuesAreIntAndDouble_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"Key1", "123"},
                {"Key2", "1,4"},
                {"Key3", "4234,234"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig4>();

            var config = sut.WithAppSettings().Populate();
            
            Assert.AreEqual(int.Parse(dict.ElementAt(0).Value), config.Key1);
            Assert.AreEqual(ConvertType<double>(dict.ElementAt(1).Value, TypeCode.Double), config.Key2);
            Assert.AreEqual(ConvertType<double>(dict.ElementAt(2).Value, TypeCode.Double), config.Key3);
        }

        private T ConvertType<T>(string val, TypeCode typeCode)
        {
            return (T) Convert.ChangeType(val, TypeCode.Double, CultureInfo.InvariantCulture);
        }

        [Test]
        public void WithAppSettings_WhenPropertiesAreStringAndIntAndBool_AndAppSettingValuesAreStringAndIntAndBool_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"Key1", "foo"},
                {"Key2", "123"},
                {"Key3", "true"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig5>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.Key1);
            Assert.AreEqual(int.Parse(dict.ElementAt(1).Value), config.Key2);
            Assert.AreEqual(bool.Parse(dict.ElementAt(2).Value), config.Key3);
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfString_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"Key1", "foo;bar;hello;world"}
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            var list = new List<string>
            {
                "foo", "bar", "hello", "world"
            };

            Assert.AreEqual(list.ElementAt(0), config.Key1.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.Key1.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.Key1.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.Key1.ElementAt(3));
        }

        #endregion

        #region WithAppSettings (ConfigurationBuilder<T> overload) Method

        [TestCase("Key1", "Key1_foo", "Value1")]
        [TestCase("Key2", "KEY2_bar", "Value2")]
        [TestCase("Key3", "kEy3_heLLO", "Value3")]
        public void WithAppSettings_WithConfigurationMapProvided(string propertyName, string mapsToKey, string appSettingValue)
        {
            var appSettings = new Dictionary<string, string>
            {
                {"Key1_foo", "Value1"},
                {"Key2_bar", "Value2"},
                {"Key3_hello", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(appSettings);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(appSettings.ElementAt(0).Key.ToLower())).Returns(appSettings.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(appSettings.ElementAt(1).Key.ToLower())).Returns(appSettings.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(appSettings.ElementAt(2).Key.ToLower())).Returns(appSettings.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig>();

            var expr = GetExpression(propertyName);

            var config = sut.WithAppSettings(c => c.Map(expr, mapsToKey)).Populate();

            Assert.AreEqual(appSettingValue, config.Key1);
            
        }

        private Expression<Func<ConfigurationLoaderFixture.TestConfig, object>> GetExpression(string prop)
        {
            var property = typeof(ConfigurationLoaderFixture.TestConfig).GetProperties().SingleOrDefault(p => p.Name == prop);
            
            if (property != null)
            {
                return c => c.Key1;
            }
            throw new Exception("Couldnt create expression, check property name");
        }

        #endregion

        #region WithConnectionStrings Method

        [Test]
        public void WithConnectionStrings_CallsGetConnectionStringOnKonfiggy()
        {
            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig>();

            sut.WithConnectionStrings();

            _fixture.Konfiggy.Verify(x => x.GetConnectionString(It.IsAny<string>()));
        }

        [Test]
        public void WithConnectionStrings_WhenPropertiesArePascalCase_AndAppSettingKeysMatchPropertyCase_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"Key1", "Value1"},
                {"Key2", "Value2"},
                {"Key3", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("connectionStrings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig>();

            var config = sut.WithConnectionStrings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.Key1);
            Assert.AreEqual(dict.ElementAt(1).Value, config.Key2);
            Assert.AreEqual(dict.ElementAt(2).Value, config.Key3);
        }

        [Test]
        public void WithConnectionStrings_WhenPropertiesArePascalCase_AndAppSettingKeysAreLowerCase_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"key1", "Value1"},
                {"key2", "Value2"},
                {"key3", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("connectionStrings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig>();

            var config = sut.WithConnectionStrings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.Key1);
            Assert.AreEqual(dict.ElementAt(1).Value, config.Key2);
            Assert.AreEqual(dict.ElementAt(2).Value, config.Key3);
        }

        [Test]
        public void WithConnectionStrings_WhenPropertiesArePascalCase_AndAppSettingKeysAreInvariantCase_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"kEy1", "Value1"},
                {"KeY2", "Value2"},
                {"KEY3", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("connectionStrings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig>();

            var config = sut.WithConnectionStrings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.Key1);
            Assert.AreEqual(dict.ElementAt(1).Value, config.Key2);
            Assert.AreEqual(dict.ElementAt(2).Value, config.Key3);
        }

        [Test]
        public void WithConnectionStrings_WhenPropertiesAreLowerCase_AndAppSettingKeysAreLowerCase_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"key1", "Value1"},
                {"key2", "Value2"},
                {"key3", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("connectionStrings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig2>();

            var config = sut.WithConnectionStrings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.key1);
            Assert.AreEqual(dict.ElementAt(1).Value, config.key2);
            Assert.AreEqual(dict.ElementAt(2).Value, config.key3);
        }

        [Test]
        public void WithConnectionStrings_WhenPropertiesAreLowerCase_AndAppSettingKeysAreInvariantCase_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"kEy1", "Value1"},
                {"KeY2", "Value2"},
                {"KEY3", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("connectionStrings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig2>();

            var config = sut.WithConnectionStrings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.key1);
            Assert.AreEqual(dict.ElementAt(1).Value, config.key2);
            Assert.AreEqual(dict.ElementAt(2).Value, config.key3);
        }

        [Test]
        public void WithConnectionStrings_WhenPropertiesAreInvariantCase_AndAppSettingKeysAreInvariantCase_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"kEy1", "Value1"},
                {"KeY2", "Value2"},
                {"KEY3", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("connectionStrings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(1).Key.ToLower())).Returns(dict.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(dict.ElementAt(2).Key.ToLower())).Returns(dict.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig3>();

            var config = sut.WithConnectionStrings().Populate();

            Assert.AreEqual(dict.ElementAt(0).Value, config.KEY1);
            Assert.AreEqual(dict.ElementAt(1).Value, config.KeY2);
            Assert.AreEqual(dict.ElementAt(2).Value, config.kEy3);
        }

        #endregion
    }
}

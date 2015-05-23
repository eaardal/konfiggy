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

        #region WithAppSettings Method

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
                {"Strings", "foo;bar;hello;world"}
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            var list = new List<string>
            {
                "foo", "bar", "hello", "world"
            };

            Assert.AreEqual(list.ElementAt(0), config.Strings.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.Strings.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.Strings.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.Strings.ElementAt(3));
        }
        
        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfInt_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string>
            {
                {"Ints", "1;2;3;4"}
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            var list = new List<int>
            {
                1,2,3,4
            };

            Assert.AreEqual(list.ElementAt(0), config.Ints.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.Ints.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.Ints.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.Ints.ElementAt(3));
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfBool_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { {"Bools", "true;false;true;false"} };
            var list = new List<bool> { true, false, true, false };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();
            
            Assert.AreEqual(list.ElementAt(0), config.Bools.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.Bools.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.Bools.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.Bools.ElementAt(3));
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfBytes_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { { "Bytes", "1;2;3;4" } };
            var list = new List<byte> { 1,2,3,4 };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(list.ElementAt(0), config.Bytes.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.Bytes.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.Bytes.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.Bytes.ElementAt(3));
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfSBytes_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { { "SBytes", "1;2;3;4" } };
            var list = new List<sbyte> { 1, 2, 3, 4 };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(list.ElementAt(0), config.SBytes.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.SBytes.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.SBytes.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.SBytes.ElementAt(3));
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfUInts_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { { "UInts", "1;2;3;4" } };
            var list = new List<uint> { 1, 2, 3, 4 };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(list.ElementAt(0), config.UInts.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.UInts.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.UInts.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.UInts.ElementAt(3));
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfShorts_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { { "Shorts", "1;2;3;4" } };
            var list = new List<short> { 1, 2, 3, 4 };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(list.ElementAt(0), config.Shorts.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.Shorts.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.Shorts.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.Shorts.ElementAt(3));
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfUShorts_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { { "UShorts", "1;2;3;4" } };
            var list = new List<ushort> { 1, 2, 3, 4 };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(list.ElementAt(0), config.UShorts.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.UShorts.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.UShorts.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.UShorts.ElementAt(3));
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfLongs_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { { "Longs", "1;2;3;4" } };
            var list = new List<long> { 1, 2, 3, 4 };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(list.ElementAt(0), config.Longs.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.Longs.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.Longs.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.Longs.ElementAt(3));
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfULongs_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { { "ULongs", "1;2;3;4" } };
            var list = new List<ulong> { 1, 2, 3, 4 };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(list.ElementAt(0), config.ULongs.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.ULongs.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.ULongs.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.ULongs.ElementAt(3));
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfFloats_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { { "Floats", "1;2;3;4" } };
            var list = new List<float> { 1, 2, 3, 4 };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(list.ElementAt(0), config.Floats.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.Floats.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.Floats.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.Floats.ElementAt(3));
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfDoubles_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { { "Doubles", "1.56; 2.76; 3.43; 4.11312" } };
            var list = new List<double> { 1.56, 2.76, 3.43, 4.11312 };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(list.ElementAt(0), config.Doubles.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.Doubles.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.Doubles.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.Doubles.ElementAt(3));
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfChars_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { { "Chars", "a;b;c;d" } };
            var list = new List<char> { 'a','b','c','d' };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(list.ElementAt(0), config.Chars.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.Chars.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.Chars.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.Chars.ElementAt(3));
        }

        [Test]
        public void WithAppSettings_WhenPropertyIsIEnumerableOfDecimals_AndAppSettingValueIsSemiColonSeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { { "Decimals", "1.3;2.31;3.13;4.45323" } };
            var list = new List<decimal> { new decimal(1.3), new decimal(2.31), new decimal(3.13), new decimal(4.45323) };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig6>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(list.ElementAt(0), config.Decimals.ElementAt(0));
            Assert.AreEqual(list.ElementAt(1), config.Decimals.ElementAt(1));
            Assert.AreEqual(list.ElementAt(2), config.Decimals.ElementAt(2));
            Assert.AreEqual(list.ElementAt(3), config.Decimals.ElementAt(3));
        }

        [Test, Ignore("TODO: Dictionary parsing")]
        public void WithAppSettings_WhenPropertyIsStringStringKeyValueCollection_AndAppSettingValueIsDictionarySeparatedList_SetsPropertiesAsExpected()
        {
            var dict = new Dictionary<string, string> { { "StringString", "foo:bar; hello:world; abc:xyz" } };
            var list = new Dictionary<string, string> { { "foo", "bar"}, { "hello", "world"}, { "abc", "xyz"} };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("appSettings")).Returns(dict);
            _fixture.Konfiggy.Setup(x => x.GetAppSetting(dict.ElementAt(0).Key.ToLower())).Returns(dict.ElementAt(0).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig7>();

            var config = sut.WithAppSettings().Populate();

            Assert.AreEqual(list.ElementAt(0).Key, config.StringString.ElementAt(0).Key);
            Assert.AreEqual(list.ElementAt(0).Value, config.StringString.ElementAt(0).Value);

            Assert.AreEqual(list.ElementAt(1).Key, config.StringString.ElementAt(1).Key);
            Assert.AreEqual(list.ElementAt(1).Value, config.StringString.ElementAt(1).Value);

            Assert.AreEqual(list.ElementAt(2).Key, config.StringString.ElementAt(2).Key);
            Assert.AreEqual(list.ElementAt(2).Value, config.StringString.ElementAt(2).Value);
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

        #region WithConnectionStrings (ConfigurationBuilder<T> overload) Method

        [TestCase("Key1", "Key1_foo", "Value1")]
        [TestCase("Key2", "KEY2_bar", "Value2")]
        [TestCase("Key3", "kEy3_heLLO", "Value3")]
        public void WithConnectionStrings_WithConfigurationMapProvided(string propertyName, string mapsToKey, string appSettingValue)
        {
            var connStrings = new Dictionary<string, string>
            {
                {"Key1_foo", "Value1"},
                {"Key2_bar", "Value2"},
                {"Key3_hello", "Value3"},
            };

            _fixture.ConfigurationKeeper.Setup(x => x.GetSection("connectionStrings")).Returns(connStrings);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(connStrings.ElementAt(0).Key.ToLower())).Returns(connStrings.ElementAt(0).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(connStrings.ElementAt(1).Key.ToLower())).Returns(connStrings.ElementAt(1).Value);
            _fixture.Konfiggy.Setup(x => x.GetConnectionString(connStrings.ElementAt(2).Key.ToLower())).Returns(connStrings.ElementAt(2).Value);

            var sut = _fixture.CreateSut<ConfigurationLoaderFixture.TestConfig>();

            var expr = GetExpression(propertyName);

            var config = sut.WithConnectionStrings(c => c.Map(expr, mapsToKey)).Populate();

            Assert.AreEqual(appSettingValue, config.Key1);
        }

        #endregion
    }
}

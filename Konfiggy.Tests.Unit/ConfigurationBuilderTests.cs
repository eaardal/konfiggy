using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Konfiggy.Tests.Unit.Fixtures;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit
{
    [TestFixture]
    public class ConfigurationBuilderTests
    {
        private ConfigurationBuilderFixture _fixture;
        
        [SetUp]
        public void SetUp()
        {
            _fixture = new ConfigurationBuilderFixture();
        }

        [Test]
        public void Build_ReturnsEmptyDictionaryByDefault()
        {
            var sut = _fixture.CreateSut<ConfigurationBuilderFixture.TestConfig1>();

            var result = sut.Build();

            Assert.NotNull(result);
            Assert.IsInstanceOf<Dictionary<string, string>>(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void Map_WhenExpressionIsAMethodCall_ThrowsException()
        {
            var sut = _fixture.CreateSut<ConfigurationBuilderFixture.TestConfig1>();

            Assert.Throws<InvalidOperationException>(() => sut.Map(c => c.A(), "foo")); // throws because A() is a method, not a property
        }

        [Test]
        public void Map_WhenExpressionReturnsValue_ThrowsException()
        {
            var sut = _fixture.CreateSut<ConfigurationBuilderFixture.TestConfig1>();

            Assert.Throws<InvalidOperationException>(() => sut.Map(c => "foo", "bar"));
        }

        [Test]
        public void Map_WhenExpressionReturnsProperty_AndAppSettingKeyParamIsValid_SetsPropertyNameAndAppSettingKeyInDictionary()
        {
            var sut = _fixture.CreateSut<ConfigurationBuilderFixture.TestConfig1>();

            var result = sut.Map(x => x.Foo, "bar").Build();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("foo", result.ElementAt(0).Key);
            Assert.AreEqual("bar", result.ElementAt(0).Value);
        }

        [Test]
        public void Map_WhenExpressionReturnsProperty_AndAppSettingKeyParamIsNull_ThrowsException()
        {
            var sut = _fixture.CreateSut<ConfigurationBuilderFixture.TestConfig1>();

            Assert.Throws<ArgumentNullException>(() => sut.Map(x => x.Foo, null));
        }

        [Test]
        public void Map_WhenExpressionReturnsProperty_AndAppSettingKeyParamIsEmptyString_ThrowsException()
        {
            var sut = _fixture.CreateSut<ConfigurationBuilderFixture.TestConfig1>();

            Assert.Throws<ArgumentNullException>(() => sut.Map(x => x.Foo, string.Empty));
        }

        [Test]
        public void Map_KeyIsLowercase()
        {
            var sut = _fixture.CreateSut<ConfigurationBuilderFixture.TestConfig1>();
            
            var result = sut.Map(x => x.Foo, "bar").Build();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("foo", result.ElementAt(0).Key);
        }

        [Test]
        public void Map_ValueIsLowercase()
        {
            var sut = _fixture.CreateSut<ConfigurationBuilderFixture.TestConfig1>();
            
            var result = sut.Map(x => x.Foo, "BAR").Build();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("bar", result.ElementAt(0).Value);
        }
    }
}

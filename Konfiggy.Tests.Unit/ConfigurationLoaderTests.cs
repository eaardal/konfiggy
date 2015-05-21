using System;
using System.Collections.Generic;
using System.Linq;
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

        [Test]
        public void CreatesSystemUnderTest()
        {
            var sut = _fixture.CreateSut();
            Assert.NotNull(sut);
            Assert.IsInstanceOf<ConfigurationLoader<ConfigurationLoaderFixture.TestConfig>>(sut);
        }

        [Test]
        public void Load_ReturnsInstanceOfProvidedType()
        {
            var sut = _fixture.CreateSut();

            var config = sut.Load();

            Assert.NotNull(config);
            Assert.IsInstanceOf<ConfigurationLoaderFixture.TestConfig>(config);
        }

        [Test]
        public void WithAppSettings_GetsAppSettingsFromConfigurationKeeper()
        {
            var sut = _fixture.CreateSut();

            var loader = sut.WithAppSettings();

            _fixture.ConfigurationKeeper.Verify(x => x.GetSection("appSettings"), Times.Once);
        }
    }
}

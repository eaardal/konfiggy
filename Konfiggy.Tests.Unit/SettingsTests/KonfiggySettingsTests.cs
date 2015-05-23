using System.Globalization;
using KonfiggyFramework.Settings;
using NUnit.Framework;

namespace Konfiggy.Tests.Unit.SettingsTests
{
    [TestFixture]
    public class KonfiggySettingsTests
    {
        [Test]
        public void CultureIsInvariantCulture()
        {
            Assert.AreSame(CultureInfo.InvariantCulture, KonfiggySettings.Culture);
        }

        [Test]
        public void ConfigListValueSeparator()
        {
            Assert.AreEqual(';', KonfiggySettings.ConfigListValueSeparator);
        }

        [Test]
        public void ConfigDictionarySeparator()
        {
            Assert.AreEqual(':', KonfiggySettings.ConfigDictionarySeparator);
        }
    }
}

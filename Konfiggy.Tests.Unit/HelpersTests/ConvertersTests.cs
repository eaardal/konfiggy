using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Konfiggy.Helpers;

namespace Konfiggy.Tests.Unit.HelpersTests
{
    [TestFixture]
    public class ConvertersTests
    {
        [Test]
        public void ConvertToDictionary_ForConnectionStringsSection_PutsAllConnStringsIntoDictionary()
        {
            var connectionStrings = GetFakeConnectionStringsCollection();

            var dictionary = Converters.ConvertToDictionary(connectionStrings);

            Assert.AreEqual(connectionStrings.Count, dictionary.Count);

            Assert.AreEqual(connectionStrings[0].Name, dictionary.ElementAt(0).Key);
            Assert.AreEqual(connectionStrings[0].ConnectionString, dictionary.ElementAt(0).Value);

            Assert.AreEqual(connectionStrings[1].Name, dictionary.ElementAt(1).Key);
            Assert.AreEqual(connectionStrings[1].ConnectionString, dictionary.ElementAt(1).Value);

            Assert.AreEqual(connectionStrings[2].Name, dictionary.ElementAt(2).Key);
            Assert.AreEqual(connectionStrings[2].ConnectionString, dictionary.ElementAt(2).Value);
        }

        [Test]
        public void ConvertToDictionary_ForNameValueCollection_PutsAllNameValuePairsIntoDictionary()
        {
            var nameValues = GetFakeNameValueCollection();

            var dictionary = Converters.ConvertToDictionary(nameValues);

            Assert.AreEqual(nameValues.Count, dictionary.Count);

            Assert.AreEqual(nameValues.GetKey(0), dictionary.ElementAt(0).Key);
            Assert.AreEqual(nameValues[0], dictionary.ElementAt(0).Value);

            Assert.AreEqual(nameValues.GetKey(1), dictionary.ElementAt(1).Key);
            Assert.AreEqual(nameValues[1], dictionary.ElementAt(1).Value);

            Assert.AreEqual(nameValues.GetKey(2), dictionary.ElementAt(2).Key);
            Assert.AreEqual(nameValues[2], dictionary.ElementAt(2).Value);
        }

        private NameValueCollection GetFakeNameValueCollection()
        {
            return new NameValueCollection
            {
                {"Name_1", "Value_1"},
                {"Name_2", "Value_2"},
                {"Name_3", "Value_3"},
            };
        }

        private ConnectionStringSettingsCollection GetFakeConnectionStringsCollection()
        {
            var connectionStrings = new ConnectionStringSettingsCollection
            {
                new ConnectionStringSettings("MyDb_1", "MyConnString_1"),
                new ConnectionStringSettings("MyDb_2", "MyConnString_2"),
                new ConnectionStringSettings("MyDb_3", "MyConnString_3")
            };
            return connectionStrings;
        }
    }
}

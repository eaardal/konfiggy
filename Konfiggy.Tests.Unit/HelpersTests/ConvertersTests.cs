using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonfiggyFramework.Helpers;
using NUnit.Framework;

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

        [Test]
        public void ToExpando_ContainsExpectedKeyValueCount()
        {
            var nameValues = GetFakeNameValueCollection();

            var dictionary = Converters.ConvertToDictionary(nameValues);
            dynamic dynamicDict = dictionary.ToExpando();

            var count = 0;
            foreach (var property in dynamicDict)
            {
                count++;
            }

            Assert.AreEqual(3, count);
        }

        [Test]
        public void ToExpando_ContainsExpectedKeyValues()
        {
            var nameValues = GetFakeNameValueCollection();

            var dictionary = Converters.ConvertToDictionary(nameValues);
            dynamic dynamicDict = dictionary.ToExpando();
            
            Assert.NotNull(dynamicDict.Name_1);
            Assert.AreEqual(nameValues[0], dynamicDict.Name_1);

            Assert.NotNull(dynamicDict.Name_2);
            Assert.AreEqual(nameValues[1], dynamicDict.Name_2);

            Assert.NotNull(dynamicDict.Name_3);
            Assert.AreEqual(nameValues[2], dynamicDict.Name_3);
        }

        [Test]
        public void ToExpando_RemovesDotInKeys()
        {
            var nameValues = new NameValueCollection
            {
                {"a.foo", "Value_1"},
                {"b.foo", "Value_2"},
                {"c.foo", "Value_3"},
            };

            var dictionary = Converters.ConvertToDictionary(nameValues);
            dynamic dynamicDict = dictionary.ToExpando();

            PrintProperties(dynamicDict);

            Assert.NotNull(dynamicDict.AFoo);
            Assert.AreEqual(nameValues[0], dynamicDict.AFoo);

            Assert.NotNull(dynamicDict.BFoo);
            Assert.AreEqual(nameValues[1], dynamicDict.BFoo);

            Assert.NotNull(dynamicDict.CFoo);
            Assert.AreEqual(nameValues[2], dynamicDict.CFoo);
        }

        private void PrintProperties(dynamic obj)
        {
            foreach (var prop in obj)
            {
                Console.WriteLine(prop.Key + ": " + prop.Value);
            }
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

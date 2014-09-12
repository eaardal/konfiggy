using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace Konfiggy.Core.Helpers
{
    public static class Converters
    {
        public static IDictionary<string, string> ConvertToDictionary(ConnectionStringSettingsCollection connectionStrings)
        {
            var convertAction = new Action<IDictionary<string, string>>(dictionary =>
            {
                for (int i = 0; i < connectionStrings.Count; i++)
                    dictionary.Add(connectionStrings[i].Name, connectionStrings[i].ConnectionString);
            });
            return ConvertToDictionary(convertAction);
        }

        public static IDictionary<string, string> ConvertToDictionary(NameValueCollection nameValueCollection)
        {
            var convertAction = new Action<IDictionary<string, string>>(dictionary =>
            {
                for (int i = 0; i < nameValueCollection.Count; i++)
                    dictionary.Add(nameValueCollection.GetKey(i), nameValueCollection[i]);
            });
            return ConvertToDictionary(convertAction);
        }

        private static IDictionary<string, string> ConvertToDictionary(Action<IDictionary<string, string>> fillDictionaryAction)
        {
            var dictionary = new Dictionary<string, string>();
            fillDictionaryAction(dictionary);
            return dictionary;
        }
    }
}

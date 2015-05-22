namespace KonfiggyFramework.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Dynamic;
    using System.Globalization;
    using System.Threading;

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

        public static ExpandoObject ToExpando(this IDictionary<string, string> dictionary)
        {
            var expando = new ExpandoObject();
            var expandoDic = (IDictionary<string, object>)expando;

            foreach (var kvp in dictionary)
            {
                expandoDic.Add(SanitizeSettingsKey(kvp.Key), kvp.Value);
            }

            return expando;
        }

        /// Based on http://www.dotnetperls.com/uppercase-first-letter
        private static string SanitizeSettingsKey(string key)
        {
            if (key.Contains("."))
            {
                key = key.Replace(".", " ");
            }

            char[] array = key.ToCharArray();

            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }

            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }

            return new string(array).Replace(" ", "");
        }

        private static IDictionary<string, string> ConvertToDictionary(Action<IDictionary<string, string>> fillDictionaryAction)
        {
            var dictionary = new Dictionary<string, string>();
            fillDictionaryAction(dictionary);
            return dictionary;
        }
    }
}

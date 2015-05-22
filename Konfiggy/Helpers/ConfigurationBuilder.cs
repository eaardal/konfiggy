using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using KonfiggyFramework.Settings;

namespace KonfiggyFramework.Helpers
{
    public class ConfigurationBuilder<T>
    {
        private readonly Dictionary<string, string> _dictionary;

        public ConfigurationBuilder()
        {
            _dictionary = new Dictionary<string, string>();
        }

        public ConfigurationBuilder<T> Map(Expression<Func<T, object>> configExpression, string appSettingKey)
        {
            if (string.IsNullOrEmpty(appSettingKey)) throw new ArgumentNullException("appSettingKey");

            var culture = KonfiggySettings.Culture;

            var memberExpression = configExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new InvalidOperationException("Invalid configuration mapping expression. A configExpression should return a property with public get/set");
            }
            
            var key = memberExpression.Member.Name.ToLower(culture);

            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, appSettingKey.ToLower(culture));
            }

            return this;
        }

        public Dictionary<string, string> Build()
        {
            return _dictionary;
        } 
    }
}
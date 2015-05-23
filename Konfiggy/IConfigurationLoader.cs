using System;
using KonfiggyFramework.Helpers;

namespace KonfiggyFramework
{
    public interface IConfigurationLoader<T> where T : new()
    {
        T Populate();
        ConfigurationLoader<T> WithAppSettings();
        ConfigurationLoader<T> WithConnectionStrings();
        ConfigurationLoader<T> WithAppSettings(Func<ConfigurationBuilder<T>, ConfigurationBuilder<T>> appSettingToConfigMap);
        ConfigurationLoader<T> WithConnectionStrings(Func<ConfigurationBuilder<T>, ConfigurationBuilder<T>> connStringToConfigMap);
    }
}
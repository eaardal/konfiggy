namespace KonfiggyFramework
{
    public interface IConfigurationLoader<T> where T : new()
    {
        T Load();
        ConfigurationLoader<T> WithAppSettings();
        ConfigurationLoader<T> WithConnectionStrings();
    }
}
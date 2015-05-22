using KonfiggyFramework.KeyValueRetrievalStrategies;
using KonfiggyFramework.TagStrategies;

namespace KonfiggyFramework
{
    using System.Collections.Generic;
    using System.Dynamic;

    public interface IKonfiggy
    {
        /// <summary>
        /// The <see cref="IEnvironmentTagStrategy"/> describes how Konfiggy will look for and retrieve the Konfiggy Environment Tag.
        /// The Environment Tag indicates the current environment such as "Dev", "QA", "Prod" etc. Its value will be used when retrieving
        /// an entry from app/web.config. If you ask for the app setting "MyFile" for example, and the <see cref="IEnvironmentTagStrategy"/> 
        /// resolves the Environment Tag to be "Prod", Konfiggy will look for an app setting entry called "Prod.MyFile".
        /// </summary>
        IEnvironmentTagStrategy EnvironmentTagStrategy { get; set; }

        /// <summary>
        /// The <see cref="IConfigurationKeeper"/> is a wrapper around the mechanic to get the actual data containing the 
        /// environment-aware settings and configurations.
        /// By default this is using <see cref="System.Configuration.ConfigurationManager"/> to retrieve configuration info
        /// </summary>
        IConfigurationKeeper ConfigurationKeeper { get; set; }

        /// <summary>
        /// Get a connection string by its name. By default this looks in the app/web.config's connectionStrings section.
        /// </summary>
        /// <param name="name">The name of the connection string to look for</param>
        /// <returns>Returns the connection string found matching the name and the current Environment Tag</returns>
        string GetConnectionString(string name);

        /// <summary>
        /// Get an <see cref="T:System.Collections.IDictionary"/>{TKey, TValue} of all connection strings.
        ///  By default this looks in the app/web.config's connectionStrings section.
        /// </summary>
        /// <returns>Returns all the settings in the app's app/web.config connectionStrings section</returns>
        IDictionary<string, string> GetConnectionStrings();

        /// <summary>
        /// Get an <see cref="T:System.Dynamic.ExpandoObject"/> whose properties
        /// are the connectionstrings in the connectionStrings section of the
        /// app/web.config. By default this looks in the app/web.config's
        /// connectionStrings section.
        /// </summary>
        /// <returns>Returns all the connectionstrings in the app's
        /// app/web.config connectionStrings section as properties on an 
        /// <see cref="T:System.Dynamic.ExpandoObject"/></returns>
        ExpandoObject GetConnectionStringsDynamic();

        /// <summary>
        /// Get an app setting entry by its key. By default this looks in the app/web.config's appSettings section.
        /// </summary>
        /// <param name="key">The key of the key-value entry to look for.</param>
        /// <returns>Returns the value of the key-value entry matching the key given and the current Environment Tag</returns>
        string GetAppSetting(string key);

        /// <summary>
        /// Get an <see cref="T:System.Collections.IDictionary"/>{TKey, TValue} of all app settings.
        ///  By default this looks in the app/web.config's appSettings section.
        /// </summary>
        /// <returns>Returns all the settings in the app's app/web.config appSettings section</returns>
        IDictionary<string, string> GetAppSettings();

        /// <summary>
        /// Get an <see cref="T:System.Dynamic.ExpandoObject"/> whose properties
        /// are the settings in the appSettings section of the app/web.config.
        /// By default this looks in the app/web.config's appSettings section.
        /// </summary>
        /// <returns>Returns all the settings in the app's app/web.config
        /// appSettings section as properties on an 
        /// <see cref="T:System.Dynamic.ExpandoObject"/></returns>
        ExpandoObject GetAppSettingsDynamic();

        /// <summary>
        /// Get the value in a key-value collection matching the key given. Please provide the functionality for retrieving the key-value collection.  
        /// </summary>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="keyValueRetrievalStrategy">The custom implementation to use for retrieving the key-value collection. 
        /// The current IConfigurationKeeper instance will be given as input parameter. </param>
        /// <returns>Returns the value of the key-value entry matching the key given and the current Environment Tag.</returns>
        string GetCustom(string key, IKeyValueRetrievalStrategy keyValueRetrievalStrategy);

        IConfigurationLoader<T> PopulateConfig<T>() where T : new();
    }
}
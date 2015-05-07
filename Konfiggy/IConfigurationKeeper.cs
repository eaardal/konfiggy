namespace KonfiggyFramework
{
    /// <summary>
    /// The default configuration keeper for Konfiggy. A thin wrapper around <see cref="System.Configuration.ConfigurationManager"/>
    /// </summary>
    public interface IConfigurationKeeper
    {
        /// <summary>
        /// Calls into the GetSection(string name) method of <see cref="System.Configuration.ConfigurationManager"/>
        /// </summary>
        /// <param name="name">The name of the section to retrieve</param>
        /// <returns>Returns the configuraiton section matching the name given.</returns>
        object GetSection(string name);
    }
}

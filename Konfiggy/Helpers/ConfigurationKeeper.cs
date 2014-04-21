using System.Configuration;

namespace Konfiggy.Helpers
{
    /// <summary>
    /// The default configuration keeper for Konfiggy. A thin wrapper around <see cref="System.Configuration.ConfigurationManager"/> in order to make it mockable
    /// </summary>
    public class ConfigurationKeeper : IConfigurationKeeper
    {
        /// <summary>
        /// Calls into the GetSection(string name) method of <see cref="System.Configuration.ConfigurationManager"/>
        /// </summary>
        /// <param name="name">The name of the section to retrieve</param>
        /// <returns>Returns the configuraiton section matching the name given.</returns>
        public object GetSection(string name)
        {
            return ConfigurationManager.GetSection(name);
        }
    }
}

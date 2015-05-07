using KonfiggyFramework.Config;

namespace KonfiggyFramework.TagStrategies
{
    /// <summary>
    /// Resolves the Environment Tag by looking in the provided <see cref="IConfigSection"/>. 
    /// By default this looks for a named "konfiggy"-section in the app/web.config file
    /// </summary>
    public class ConfigFileTagStrategy : IEnvironmentTagStrategy
    {
        /// <summary>
        /// The Config Section to use when looking for a Konfiggy element. Uses default "konfiggy"-section if no other implementation is provided.
        /// </summary>
        public IConfigSection ConfigSection { get; set; }

        /// <summary>
        /// Gets the Environment Tag using the provided configuration
        /// </summary>
        /// <returns>Returns the Environment Tag</returns>
        public string GetEnvironmentTag()
        {
            if (ConfigSection == null) 
                ConfigSection = Config.ConfigSection.GetConfig();

            return ConfigSection.EnvironmentTag.Value;
        }
    }
}

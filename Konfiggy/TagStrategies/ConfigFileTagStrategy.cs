using KonfiggyFramework.Config;
using KonfiggyFramework.Exceptions;

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

            if (ConfigSection == null)
                throw new KonfiggyConfigSectionNotSetException("Expected a konfiggy-section to be defined in app/web.config when using the environment tag strategy " + GetType().FullName);

            return ConfigSection.EnvironmentTag.Value;
        }
    }
}

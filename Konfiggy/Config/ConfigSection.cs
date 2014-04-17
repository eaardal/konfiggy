using System.Configuration;
using Konfiggy.Helpers;

namespace Konfiggy.Config
{
    /// <summary>
    /// Represents the Konfiggy section in the app.config file.
    /// </summary>
    public class ConfigSection : ConfigurationSection, IConfigSection
    {
        public static IConfigurationKeeper ConfigurationKeeper { get; set; }

        static ConfigSection()
        {
            ConfigurationKeeper = new ConfigurationKeeper();
        }

        /// <summary>
        /// Gets the Konfiggy section from the app.config.
        /// </summary>
        /// <returns></returns>
        public static IConfigSection GetConfig()
        {
            return (IConfigSection)ConfigurationKeeper.GetSection("konfiggy");
        }

        [ConfigurationProperty("environmentTag")]
        public IEnvironmentTagElement EnvironmentTag
        {
            get { return this["environmentTag"] as IEnvironmentTagElement; }
        }
    }

    /// <summary>
    /// Represents a EnvironmentTag element in a Konfiggy configuration section in the app.config
    /// </summary>
    public class EnvironmentTagElement : ConfigurationElement, IEnvironmentTagElement
    {
        /// <summary>
        /// The environment tag value
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true, IsKey = true)]
        public string Value
        {
            get { return this["value"] as string; }
        }
    }

}

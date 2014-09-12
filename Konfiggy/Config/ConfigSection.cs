using System.Configuration;

namespace KonfiggyFramework.Config
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
        public EnvironmentTagElement EnvironmentTag
        {
            get { return this["environmentTag"] as EnvironmentTagElement; }
        }
    }
}

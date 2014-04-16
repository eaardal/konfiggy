using System.Configuration;

namespace Konfiggy
{
    /// <summary>
    /// Represents the Konfiggy section in the app.config file.
    /// </summary>
    public class KonfiggyConfigSection : ConfigurationSection
    {
        /// <summary>
        /// Gets the Konfiggy section from the app.config.
        /// </summary>
        /// <returns></returns>
        public static KonfiggyConfigSection GetConfig()
        {
            return (KonfiggyConfigSection)ConfigurationManager.GetSection("konfiggy");
        }

        [ConfigurationProperty("environmentTag")]
        public EnvironmentTagElement EnvironmentTag
        {
            get { return this["environmentTag"] as EnvironmentTagElement; }
        }
    }

    /// <summary>
    /// Represents a EnvironmentTag element in a Konfiggy configuration section in the app.config
    /// </summary>
    public class EnvironmentTagElement : ConfigurationElement
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

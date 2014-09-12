using System.Configuration;

namespace KonfiggyFramework.Config
{
    /// <summary>
    /// Represents a EnvironmentTag element in a Konfiggy configuration section in the app.config
    /// </summary>
    public class EnvironmentTagElement : ConfigurationElement, IEnvironmentTagElement
    {
        /// <summary>
        /// The environment tag value
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true, IsKey = true)]
        public virtual string Value
        {
            get { return this["value"] as string; }
        }
    }
}
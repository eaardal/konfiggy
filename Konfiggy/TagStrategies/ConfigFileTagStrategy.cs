using Konfiggy.Config;

namespace Konfiggy.TagStrategies
{
    public class ConfigFileTagStrategy : IEnvironmentTagStrategy
    {
        public IConfigSection ConfigSection { get; set; }

        public string GetEnvironmentTag()
        {
            if (ConfigSection == null) 
                ConfigSection = Config.ConfigSection.GetConfig();

            return ConfigSection.EnvironmentTag.Value;
        }
    }
}

using System;

namespace Konfiggy.TagStrategies
{
    public class GlobalConfigFileVariableTagStrategy : IEnvironmentTagStrategy
    {
        public string GetEnvironmentTag()
        {
            try
            {
                var configSection = KonfiggyConfigSection.GetConfig();
                var tag = configSection.EnvironmentTag;
                return tag.Value;
            }
            catch (Exception ex)
            { 
                throw;
            }
        }
    }
}

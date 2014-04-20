using Konfiggy.Helpers;

namespace Konfiggy.TagStrategies
{
    public class KonfiggyXmlFileTagStrategy : FileTagStrategy
    {
        public override string GetEnvironmentTag()
        {
            return XmlFileHelpers.GetEnvironmentTag(FilePath);
        }
    }
}
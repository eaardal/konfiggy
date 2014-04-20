using Konfiggy.Helpers;

namespace Konfiggy.TagStrategies
{
    public class TextFileTagStrategy : FileTagStrategy
    {
        public override string GetEnvironmentTag()
        {
            return TextFileHelpers.GetEnvironmentTag(FilePath);
        }
    }
}
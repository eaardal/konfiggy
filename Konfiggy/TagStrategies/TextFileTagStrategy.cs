using Konfiggy.Core.Helpers;
using Konfiggy.Core.Settings;

namespace Konfiggy.Core.TagStrategies
{
    public class TextFileTagStrategy : FileTagStrategy
    {
        public TextFileTagStrategy() { }

        public TextFileTagStrategy(IFileSettings fileSettings) : base(fileSettings) { }
        
        public override string GetEnvironmentTag()
        {
            EnsureFoldersAndFileExists();

            return TextFileHelpers.GetEnvironmentTag(FileSettings.EnvironmentTagStorageFilePath);
        }
    }
}
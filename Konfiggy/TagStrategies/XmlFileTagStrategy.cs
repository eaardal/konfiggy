using Konfiggy.Core.Helpers;
using Konfiggy.Core.Settings;

namespace Konfiggy.Core.TagStrategies
{
    class XmlFileTagStrategy : FileTagStrategy
    {
        public XmlFileTagStrategy() { }

        public XmlFileTagStrategy(IFileSettings fileSettings) : base(fileSettings) { }

        public override string GetEnvironmentTag()
        {
            EnsureFoldersAndFileExists();

            return XmlFileHelpers.GetEnvironmentTag(FileSettings.EnvironmentTagStorageFilePath);
        }
    }
}
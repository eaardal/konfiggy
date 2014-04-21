using Konfiggy.Helpers;

namespace Konfiggy.TagStrategies
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
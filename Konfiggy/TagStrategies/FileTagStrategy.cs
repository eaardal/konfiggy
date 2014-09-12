using Konfiggy.Core.Exceptions;
using Konfiggy.Core.Helpers;
using Konfiggy.Core.Settings;

namespace Konfiggy.Core.TagStrategies
{
    public abstract class FileTagStrategy : IEnvironmentTagStrategy
    {
        public IFileSettings FileSettings { get; set; }

        public abstract string GetEnvironmentTag();

        protected FileTagStrategy() { }

        protected FileTagStrategy(IFileSettings fileSettings)
        {
            FileSettings = fileSettings;
        }

        protected void EnsureFoldersAndFileExists()
        {
            if (FileSettings == null)
                throw new KonfiggyFileSettingsNotSetException("Please provide an implementation of IFileSettings in the FileSettings property before calling EnsureFoldersAndFileExists()");

            FileHelpers.EnsurePathExists(FileSettings.EnvironmentTagStorageFilePath);
            FileHelpers.CreateFileIfNotExists(FileSettings.EnvironmentTagStorageFilePath);
        }
    }
}

using KonfiggyFramework.Exceptions;
using KonfiggyFramework.Helpers;
using KonfiggyFramework.Settings;

namespace KonfiggyFramework.TagStrategies
{
    /// <summary>
    /// Base class for resolving the Environment Tag from a file.
    /// </summary>
    public abstract class FileTagStrategy : IEnvironmentTagStrategy
    {
        /// <summary>
        /// Gets or Sets the settings for the file to use.
        /// </summary>
        public IFileSettings FileSettings { get; set; }

        /// <summary>
        /// Gets the Environment Tag using the provided configuration
        /// </summary>
        /// <returns>Returns the Environment Tag</returns>
        public abstract string GetEnvironmentTag();

        protected FileTagStrategy() { }

        protected FileTagStrategy(IFileSettings fileSettings)
        {
            FileSettings = fileSettings;
        }

        /// <summary>
        /// Checks if the folderpath and file provided by the <see cref="FileSettings"/> exists. If not, it creates them.
        /// </summary>
        protected void EnsureFoldersAndFileExists()
        {
            if (FileSettings == null)
                throw new KonfiggyFileSettingsNotSetException("Please provide an implementation of IFileSettings in the FileSettings property before calling EnsureFoldersAndFileExists()");

            FileHelpers.EnsurePathExists(FileSettings.EnvironmentTagStorageFilePath);
            FileHelpers.CreateFileIfNotExists(FileSettings.EnvironmentTagStorageFilePath);
        }
    }
}

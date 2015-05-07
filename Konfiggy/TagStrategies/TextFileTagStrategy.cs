using KonfiggyFramework.Helpers;
using KonfiggyFramework.Settings;

namespace KonfiggyFramework.TagStrategies
{
    /// <summary>
    /// Resolves the Environment Tag by looking in the file set in the file settings. Expects the file to only contain the Environment Tag value.
    /// </summary>
    public class TextFileTagStrategy : FileTagStrategy
    {
        public TextFileTagStrategy() { }

        /// <summary>
        /// Creates a new instance with the provided file settings. Expects the file to only contain the Environment Tag value.
        /// </summary>
        /// <param name="fileSettings"></param>
        public TextFileTagStrategy(IFileSettings fileSettings) : base(fileSettings) { }

        /// <summary>
        /// Gets the Environment Tag using the provided configuration
        /// </summary>
        /// <returns>Returns the Environment Tag</returns>
        public override string GetEnvironmentTag()
        {
            EnsureFoldersAndFileExists();

            return TextFileHelpers.GetEnvironmentTag(FileSettings.EnvironmentTagStorageFilePath);
        }
    }
}
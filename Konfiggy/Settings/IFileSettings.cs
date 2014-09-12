namespace Konfiggy.Core.Settings
{
    public interface IFileSettings
    {
        string EnvironmentTagStorageFilePath { get; }
        KonfiggyFileType FileType { get; }
        string FileName { get; }
    }
}
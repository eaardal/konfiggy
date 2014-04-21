namespace Konfiggy.Settings
{
    public interface IFileSettings
    {
        string EnvironmentTagStorageFilePath { get; }
        KonfiggyFileType FileType { get; }
        string FileName { get; }
    }
}
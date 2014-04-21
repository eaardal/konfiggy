namespace Konfiggy
{
    public interface IFileSettings
    {
        string EnvironmentTagStorageFilePath { get; }
        KonfiggyFileType FileType { get; }
        string FileName { get; }
    }
}
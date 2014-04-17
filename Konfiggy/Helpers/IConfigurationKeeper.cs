namespace Konfiggy.Helpers
{
    public interface IConfigurationKeeper
    {
        object GetSection(string name);
    }
}

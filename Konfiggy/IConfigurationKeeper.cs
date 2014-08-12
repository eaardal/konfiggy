namespace Konfiggy
{
    public interface IConfigurationKeeper
    {
        object GetSection(string name);
    }
}

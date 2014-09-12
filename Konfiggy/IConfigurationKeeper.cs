namespace Konfiggy.Core
{
    public interface IConfigurationKeeper
    {
        object GetSection(string name);
    }
}

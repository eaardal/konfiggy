namespace KonfiggyFramework
{
    public interface IConfigurationKeeper
    {
        object GetSection(string name);
    }
}

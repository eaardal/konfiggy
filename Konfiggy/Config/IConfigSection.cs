namespace Konfiggy.Config
{
    public interface IConfigSection
    {
        IEnvironmentTagElement EnvironmentTag { get; }
    }
}
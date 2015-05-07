namespace KonfiggyFramework.TagStrategies
{
    /// <summary>
    /// Contains logic to resolve the Environment Tag
    /// </summary>
    public interface IEnvironmentTagStrategy
    {
        /// <summary>
        /// Gets the Environment Tag using the provided configuration
        /// </summary>
        /// <returns>Returns the Environment Tag</returns>
        string GetEnvironmentTag();
    }
}
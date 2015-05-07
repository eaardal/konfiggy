using System;

namespace KonfiggyFramework
{
    /// <summary>
    /// Gets system info such as environment variables and the machine name
    /// </summary>
    public interface ISystemEnvironment
    {
        /// <summary>
        /// Gets a value from the system environment variables by the given key and target
        /// </summary>
        /// <param name="variable">The name of the environment variable</param>
        /// <param name="target">The target/group that the variable belongs to</param>
        /// <returns>Returns the value, if it exists</returns>
        string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target);

        /// <summary>
        /// Gets the name of the current machine
        /// </summary>
        /// <returns>Returns the name of the current machine</returns>
        string GetMachineName();
    }
}

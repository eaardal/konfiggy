using System;

namespace KonfiggyFramework
{
    /// <summary>
    /// The default implementation to get system environment info.
    /// This is a wrapper around <see cref="System.Environment"/>
    /// </summary>
    public class SystemEnvironment : ISystemEnvironment
    {
        /// <summary>
        /// Gets a value from the system environment variables by the given key and target
        /// </summary>
        /// <param name="variable">The name of the environment variable</param>
        /// <param name="target">The target/group that the variable belongs to</param>
        /// <returns>Returns the value, if it exists</returns>
        public string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
        {
            return Environment.GetEnvironmentVariable(variable, target);
        }

        /// <summary>
        /// Gets the name of the current machine
        /// </summary>
        /// <returns>Returns the name of the current machine</returns>
        public string GetMachineName()
        {
            return Environment.MachineName;
        }
    }
}
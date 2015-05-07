using System;
using KonfiggyFramework.Exceptions;

namespace KonfiggyFramework.TagStrategies
{
    /// <summary>
    /// Resolves the Environment Tag by looking in the system's environment variables
    /// </summary>
    public class EnvironmentVariableTagStrategy : IEnvironmentTagStrategy
    {
        /// <summary>
        /// The name of the environment variable that Konfiggy should look up on a system for getting the environment tag
        /// </summary>
        public string KonfiggyIdentifier { get; set; }

        /// <summary>
        /// The system environment implementation that resolves the actual environment variable values
        /// </summary>
        public ISystemEnvironment SystemEnvironment { get; set; }

        /// <summary>
        /// Createas a new instance using the default <see cref="KonfiggyFramework.SystemEnvironment"/> implementation for the <see cref="SystemEnvironment"/> property
        /// and "Konfiggy" as the default <see cref="KonfiggyIdentifier"/> value
        /// </summary>
        public EnvironmentVariableTagStrategy()
        {
            SystemEnvironment = new SystemEnvironment();
            KonfiggyIdentifier = "Konfiggy";
        }

        /// <summary>
        /// Gets the Environment Tag using the provided configuration
        /// </summary>
        /// <returns>Returns the Environment Tag</returns>
        public string GetEnvironmentTag()
        {
            if (SystemEnvironment == null) throw new KonfiggyEnvironmentNotSetException("Please provide an implementation of ISystemEnvironment before calling GetEnvironmentTag()");
            if (String.IsNullOrEmpty(KonfiggyIdentifier)) throw new KonfiggyIdentifierNotSetException("Please provide a value for the KonfiggyIdentifier property before calling GetEnvironmentTag()");

            string value = TryGetValueFromUserVariables();
            if (!String.IsNullOrEmpty(value)) return value;

            value = TryGetValueFromSystemVariables();
            if (!String.IsNullOrEmpty(value)) return value;

            return null;
        }

        private string TryGetValueFromUserVariables()
        {
            return SystemEnvironment.GetEnvironmentVariable(KonfiggyIdentifier, EnvironmentVariableTarget.User);
        }

        private string TryGetValueFromSystemVariables()
        {
            return SystemEnvironment.GetEnvironmentVariable(KonfiggyIdentifier, EnvironmentVariableTarget.Machine);
        }
    }
}
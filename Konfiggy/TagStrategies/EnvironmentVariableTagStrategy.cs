using System;
using KonfiggyFramework.Exceptions;

namespace KonfiggyFramework.TagStrategies
{
    public class EnvironmentVariableTagStrategy : IEnvironmentTagStrategy
    {
        public string KonfiggyIdentifier { get; set; }
        public ISystemEnvironment SystemEnvironment { get; set; }

        public EnvironmentVariableTagStrategy()
        {
            SystemEnvironment = new SystemEnvironment();
            KonfiggyIdentifier = "Konfiggy";
        }

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
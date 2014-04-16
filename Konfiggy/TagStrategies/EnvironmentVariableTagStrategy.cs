using System;
using System.Diagnostics;

namespace Konfiggy.TagStrategies
{
    public class EnvironmentVariableTagStrategy : IKonfiggyTagStrategy
    {
        private const string KonfiggyIdentifier = "Konfiggy";

        public string GetEnvironmentTag()
        {
            string value = TryGetValueFromUserVariables();
            if (!String.IsNullOrEmpty(value)) return value;

            value = TryGetValueFromSystemVariables();
            if (!String.IsNullOrEmpty(value)) return value;

            return null;
        }

        private string TryGetValueFromUserVariables()
        {
            return Environment.GetEnvironmentVariable(KonfiggyIdentifier, EnvironmentVariableTarget.User);
        }

        private string TryGetValueFromSystemVariables()
        {
            return Environment.GetEnvironmentVariable(KonfiggyIdentifier, EnvironmentVariableTarget.Machine);
        }
    }
}
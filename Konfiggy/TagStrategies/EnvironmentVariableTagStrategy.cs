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

            if (String.IsNullOrEmpty(value))
            {
                Debug.WriteLine("Could not find any \"Konfiggy\" environment user variable");
                value = TryGetValueFromSystemVariables();

                if (String.IsNullOrEmpty(value))
                {
                    Debug.WriteLine("Could not find any \"Konfiggy\" environment system variable");
                    return null;
                }
            }

            Debug.WriteLine("Found \"Konfiggy\" environment variable: {0}", value);
            return value;
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
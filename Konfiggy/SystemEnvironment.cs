using System;

namespace Konfiggy.Core
{
    public class SystemEnvironment : ISystemEnvironment
    {
        public string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
        {
            return Environment.GetEnvironmentVariable(variable, target);
        }

        public string GetMachineName()
        {
            return Environment.MachineName;
        }
    }
}
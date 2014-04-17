using System;

namespace Konfiggy
{
    public class SystemEnvironment : IEnvironment
    {
        public string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
        {
            return Environment.GetEnvironmentVariable(variable, target);
        }
    }
}
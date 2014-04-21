using System;

namespace Konfiggy.Helpers
{
    public interface ISystemEnvironment
    {
        string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target);
        string GetMachineName();
    }
}

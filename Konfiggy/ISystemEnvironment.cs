using System;

namespace Konfiggy
{
    public interface ISystemEnvironment
    {
        string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target);
        string GetMachineName();
    }
}

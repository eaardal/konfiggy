using System;

namespace KonfiggyFramework
{
    public interface ISystemEnvironment
    {
        string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target);
        string GetMachineName();
    }
}

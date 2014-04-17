using System;

namespace Konfiggy
{
    public interface IEnvironment
    {
        string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target);
    }
}

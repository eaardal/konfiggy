using System;
using System.IO;
using Konfiggy.Exceptions;

namespace Konfiggy.TagStrategies
{
    public abstract class FileTagStrategy : IEnvironmentTagStrategy
    {
        public string FilePath { get; set; }
        public abstract string GetEnvironmentTag();
    }
}

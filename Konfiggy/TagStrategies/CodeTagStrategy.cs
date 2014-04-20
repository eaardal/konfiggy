using System;
using Konfiggy.Exceptions;

namespace Konfiggy.TagStrategies
{
    public class CodeTagStrategy : IEnvironmentTagStrategy
    {
        public string EnvironmentTag { get; set; }

        public CodeTagStrategy() { }
        
        public CodeTagStrategy(string environmentTag)
        {
            if (String.IsNullOrEmpty(environmentTag)) throw new ArgumentNullException();
            EnvironmentTag = environmentTag;
        }

        public string GetEnvironmentTag()
        {
            if (String.IsNullOrEmpty(EnvironmentTag))
                throw new KonfiggyEnvironmentTagNotFoundException("Please ensure that the EnvironmentTag property has a value before calling GetEnvironmentTag()");

            return EnvironmentTag;
        }
    }
}

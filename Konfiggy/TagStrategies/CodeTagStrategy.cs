using System;
using KonfiggyFramework.Exceptions;

namespace KonfiggyFramework.TagStrategies
{
    /// <summary>
    /// Resolves the Environment Tag from the <see cref="EnvironmentTag"/> property set in code
    /// </summary>
    public class CodeTagStrategy : IEnvironmentTagStrategy
    {
        /// <summary>
        /// The hard-coded Environment Tag to use
        /// </summary>
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

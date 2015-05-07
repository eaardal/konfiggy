using System;
using System.Collections.Generic;
using System.Linq;
using KonfiggyFramework.Exceptions;

namespace KonfiggyFramework.TagStrategies
{
    /// <summary>
    /// Resolves the Environment Tag by using the current machine's name and the provided dictionary
    /// </summary>
    public class MachineNameTagStrategy : IEnvironmentTagStrategy
    {
        /// <summary>
        /// Gets or Sets the dictionary that maps machine names to environment tags. For example:
        /// "192.168.1.2" -> "Dev"
        /// "192.168.1.3" -> "Prod"
        /// </summary>
        public IDictionary<string, string> MachineNamesMap { get; set; }

        /// <summary>
        /// Gets or Sets the system environment that resolves the machine name.
        /// </summary>
        public ISystemEnvironment SystemEnvironment { get; set; }

        public MachineNameTagStrategy(IDictionary<string, string> machineNamesMap)
        {
            if (machineNamesMap == null) throw new ArgumentNullException("machineNamesMap");
            MachineNamesMap = machineNamesMap;

            SystemEnvironment = new SystemEnvironment();
        }
        
        /// <summary>
        /// Gets the Environment Tag using the provided configuration
        /// </summary>
        /// <returns>Returns the Environment Tag</returns>
        public string GetEnvironmentTag()
        {
            if (MachineNamesMap == null || !MachineNamesMap.Any())
                throw new KonfiggyServerNamesMapNotSetException("Please provide one or more entries in the MachineNamesMap property before calling GetEnvironmentTag()");

            if (SystemEnvironment == null)
                throw new KonfiggyEnvironmentNotSetException("Please provde an implementation of ISystemEnvironment to the SystemEnvironment property before calling GetEnvironmentTag()");

            var machineName = GetNameOfCurrentMachine();
            var tag = FindEnvironmentTagInMap(machineName);

            return tag;
        }

        private string FindEnvironmentTagInMap(string currentMachineName)
        {
            return MachineNamesMap.FirstOrDefault(kvp => kvp.Key.ToLower().Equals(currentMachineName.ToLower())).Value;
        }

        private string GetNameOfCurrentMachine()
        {
            return SystemEnvironment.GetMachineName();
        }
    }
}

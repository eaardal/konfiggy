using System;
using System.Collections.Generic;
using System.Linq;
using Konfiggy.Core.Exceptions;

namespace Konfiggy.Core.TagStrategies
{
    public class MachineNameTagStrategy : IEnvironmentTagStrategy
    {
        public IDictionary<string, string> MachineNamesMap { get; set; }
        public ISystemEnvironment SystemEnvironment { get; set; }

        public MachineNameTagStrategy(IDictionary<string, string> machineNamesMap)
        {
            if (machineNamesMap == null) throw new ArgumentNullException("machineNamesMap");
            MachineNamesMap = machineNamesMap;

            SystemEnvironment = new SystemEnvironment();
        }

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

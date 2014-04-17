using System;
using System.Diagnostics;
using Konfiggy;
using Konfiggy.Helpers;
using Konfiggy.TagStrategies;

namespace Konfiggy.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Konfiggy.Initialize(new ConfigFileGlobalVariableTagStrategy(), new ConfigurationKeeper());

            string myFilePath = Konfiggy.GetAppSetting("MyFile");
            Console.WriteLine(myFilePath);

            Console.ReadLine();
        }
    }
}

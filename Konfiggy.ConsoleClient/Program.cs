using System;
using System.Diagnostics;
using Konfiggy;
using Konfiggy.TagStrategies;

namespace Konfiggy.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Konfiggy.Initialize(new GlobalConfigFileVariableTagStrategy());

            string myFilePath2 = Konfiggy.GetAppSetting("MyFile");
            Debug.WriteLine(myFilePath2);

            Console.ReadLine();
        }
    }
}

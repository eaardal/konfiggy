using System;
using Konfiggy.Helpers;
using Konfiggy.TagStrategies;

namespace Konfiggy.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string myFilePath = Konfiggy.GetConnectionString("MyDb");
            Console.WriteLine(myFilePath);

            Console.ReadLine();
        }
    }
}

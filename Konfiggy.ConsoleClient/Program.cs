using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using Konfiggy.Helpers;
using Konfiggy.TagStrategies;

namespace Konfiggy.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var strategy = new ServerNameTagStrategy
            {
                ServerNamesMap = new Dictionary<string, string>
                {
                    {"server1", "Local"},
                    {"server2", "Dev"},
                    {"eaardal-bouvet", "QA"},
                    {"server4", "Prod"},
                }
            };

            var tag = strategy.GetEnvironmentTag();

            Console.WriteLine(tag);

            Console.ReadLine();
        }
    }
}

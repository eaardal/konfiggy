using System;
using System.Reflection.Emit;
using Konfiggy.Helpers;
using Konfiggy.TagStrategies;

namespace Konfiggy.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlFilePath = @"C:\SourceControl\Konfiggy\FileTest\konfiggy.xml";
            string txtFilePath = @"C:\SourceControl\Konfiggy\FileTest\konfiggy.txt";

            var tagStrat = new TextFileTagStrategy {FilePath = txtFilePath};
            var tag = tagStrat.GetEnvironmentTag();

            Console.WriteLine(tag);

            TextFileHelpers.ModifyEnvironmentTag(txtFilePath, "Prod");
            Console.WriteLine(tagStrat.GetEnvironmentTag());

            Console.ReadLine();
        }
    }
}

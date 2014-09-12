using System.Linq;
using System.Xml.Linq;

namespace Konfiggy.Core.Helpers
{
    public static class XmlFileHelpers
    {
        public static void ModifyEnvironmentTag(string filepath, string newEnvironmentTag)
        {
            var text = FileHelpers.GetFileContent(filepath);

            var xmlDoc = XDocument.Parse(text);
            xmlDoc.Descendants("environmentTag").Select(element => element.Attribute("value")).Single().SetValue(newEnvironmentTag);

            var newText = xmlDoc.ToString();

            FileHelpers.CreateFileWithContent(filepath, newText);
        }
        
        public static void CreateFile(string filepath)
        {
            var content = CreateKonfiggyXml().ToString();
            FileHelpers.CreateFileWithContent(filepath, content);
        }
        
        public static string GetEnvironmentTag(string filepath)
        {
            var fileContent = FileHelpers.GetFileContent(filepath);
            var xmlDoc = XDocument.Parse(fileContent);
            return GetEnvironmentTagInXml(xmlDoc);
        }

        private static string GetEnvironmentTagInXml(XDocument xmlDocument)
        {
            return xmlDocument.Descendants("environmentTag").Select(element => element.Attribute("value")).Single().Value;
        }

        private static XDocument CreateKonfiggyXml()
        {
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "no"), new XElement("konfiggy",
                new XElement("environmentTag",
                    new XAttribute("value", "None"))));
            
            return doc;
        }
    }
}

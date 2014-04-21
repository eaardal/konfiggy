using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Konfiggy
{
    public class DefaultFileSettings : IFileSettings
    {
        public KonfiggyFileType FileType { get { return KonfiggyFileType.Txt; } }

        public string FileName { get { return String.Format("Konfiggy.{0}", FileType.ToString().ToLower()); } }

        public string EnvironmentTagStorageFilePath
        {
            get
            {
                var appDataRoamingFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var fullApplicationName = Assembly.GetExecutingAssembly().FullName;
                var applicationName = fullApplicationName.Substring(0, fullApplicationName.IndexOf(','));
                var version = Regex.Match(fullApplicationName, @"\b\d{1,5}\.\d{1,5}\.\d{1,5}\.\d{1,5}\b").Value.Replace('.', '-');
                var fullPath = String.Format(@"{0}\Konfiggy\{1}_{2}\{3}", appDataRoamingFolder, applicationName, version, FileName);
                return fullPath;
            }
        }
    }
}

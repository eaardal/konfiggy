using System;
using System.Reflection;
using System.Text.RegularExpressions;
using KonfiggyFramework.TagStrategies;

namespace KonfiggyFramework.Settings
{
    /// <summary>
    /// The default settings used when creating a file to hold the Environment Tag using the <see cref="FileTagStrategy"/> or any of its children (<see cref="TextFileTagStrategy"/>)
    /// Holds information about where to store the file, what type it is and the name of the file.
    /// <b>Even though Konfiggy will create the file at the location, you need to manually insert a Environment Tag string into the file. Otherwise it'll be empty.</b>
    /// </summary>
    public class DefaultFileSettings : IFileSettings
    {
        /// <summary>
        /// Gets the filetype to use.
        /// </summary>
        public KonfiggyFileType FileType { get { return KonfiggyFileType.Txt; } }

        /// <summary>
        /// Gets the filename to use (w/extension).
        /// </summary>
        public string FileName { get { return String.Format("Konfiggy.{0}", FileType.ToString().ToLower()); } }

        /// <summary>
        /// Gets the full path and name of the file.
        /// The default is: C:\Users\[CurrentUser]\AppData\Roaming\Konfiggy\[ExecutingDll-CurrentVersionNumber]\Konfiggy.[FileType]
        /// </summary>
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

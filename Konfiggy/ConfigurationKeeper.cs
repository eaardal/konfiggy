﻿using System.Collections.Specialized;
using System.Configuration;

namespace Konfiggy
{
    /// <summary>
    /// The default configuration keeper for Konfiggy. A thin wrapper around <see cref="System.Configuration.ConfigurationManager"/>
    /// </summary>
    public class ConfigurationKeeper : IConfigurationKeeper
    {
        /// <summary>
        /// Calls into the GetSection(string name) method of <see cref="System.Configuration.ConfigurationManager"/>
        /// </summary>
        /// <param name="name">The name of the section to retrieve</param>
        /// <returns>Returns the Name-Value collection representing the config section</returns>
        public NameValueCollection GetSection(string name)
        {
            return (NameValueCollection)ConfigurationManager.GetSection(name);
        }
    }
}

// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
// 
// Author           : Nicholas Tyler
// Created          : 10-26-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-26-2017
// 
// License          : MIT License
// ***********************************************************************

using System.IO;
using System.Xml.Serialization;
using NRTyler.CodeLibrary.Interfaces.Generic;
using NRTyler.KSP.DeltaVMap.Core.Models;

namespace NRTyler.KSP.DeltaVMap.Core.Repositories
{
    public class ApplicationSettingsRepository : IRepository<ApplicationSettings>
    {
        public ApplicationSettingsRepository(ApplicationSettings applicationSettings)
        {
            Settings = applicationSettings;
        }

        private string SettingsFileName { get; } = "Settings.xml";

        private ApplicationSettings Settings { get; }

        private XmlSerializer XmlSerializer { get; } = new XmlSerializer(typeof(ApplicationSettings));


        /// <summary>
        /// Serializes the <see cref="object"/> to a file using the specified <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to the specified location and what <see cref="FileMode"/> it's using.</param>
        /// <param name="obj">The <see cref="object"/> being serialized.</param>
        public void Serialize(Stream stream, ApplicationSettings obj)
        {
            var path   = $"{Settings.SettingsLocation}/{SettingsFileName}";
            var writer = new StreamWriter(path);

            using (writer)
            {
                XmlSerializer.Serialize(writer, obj);
            }
        }

        /// <summary>
        /// Deserializes a file using the specified <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to the specified file and what <see cref="FileMode"/> it's using.</param>
        /// <returns>The deserialized object.</returns>
        public ApplicationSettings Deserialize(Stream stream)
        {
            var path   = $"{Settings.SettingsLocation}/{SettingsFileName}";
            var reader = new StreamReader(path);
        
            using (reader)
            {
                var deserializedObject = (ApplicationSettings)XmlSerializer.Deserialize(reader);

                Settings.CurrentDirectory      = deserializedObject.CurrentDirectory;
                Settings.SettingsLocation      = deserializedObject.SettingsLocation;
                Settings.CelestialBodyLocation = deserializedObject.CelestialBodyLocation;

                return Settings;
            }
        }      
    }
}
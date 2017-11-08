// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
// 
// Author           : Nicholas Tyler
// Created          : 10-14-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-14-2017
// 
// License          : MIT License
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Models
{
    public sealed class ApplicationSettings : INotifyPropertyChanged
    {
        public ApplicationSettings()
        {
            SettingsLocation      = $"{CurrentDirectory}/Settings";
            CelestialBodyLocation = $"{CurrentDirectory}/CelestialBodies";
        }

        /// <summary>
        /// Gets the current directory that this program is located in.
        /// </summary>
        [DataMember]
        public string CurrentDirectory { get; set; } = Environment.CurrentDirectory;

        private string settingsLocation;
        private string celestialBodyLocation;

        /// <summary>
        /// Gets or sets the directory where the setting XML files for this application are located.
        /// </summary>
        [DataMember]
        public string SettingsLocation
        {
            get { return this.settingsLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value == this.settingsLocation)
                {
                    return;
                }             

                this.settingsLocation = value;

                OnPropertyChanged(nameof(SettingsLocation));
                DirectoryEx.CreateDirectoryIfNonexistent(SettingsLocation);
            }
        }

        /// <summary>
        /// Gets or sets the directory where the <see cref="CelestialBody"/> XML files are located.
        /// </summary>
        [DataMember]
        public string CelestialBodyLocation
        {
            get { return this.celestialBodyLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value == this.celestialBodyLocation)
                {
                    return;
                }

                this.celestialBodyLocation = value;

                OnPropertyChanged(nameof(CelestialBodyLocation));
                DirectoryEx.CreateDirectoryIfNonexistent(CelestialBodyLocation);
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
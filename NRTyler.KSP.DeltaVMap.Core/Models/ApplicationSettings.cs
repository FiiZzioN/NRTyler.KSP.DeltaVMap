// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
//
// Author           : Nicholas Tyler
// Created          : 10-14-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 11-16-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Models
{
    [DataContract(Name = "ApplicationSettings")]
    public sealed class ApplicationSettings : INotifyPropertyChanged
    {
        public ApplicationSettings()
        {
            SettingsLocation      = $"{CurrentDirectory}/Settings";
            CelestialBodyLocation = $"{CurrentDirectory}/CelestialBodies";
            SubwayLineLocation    = $"{CurrentDirectory}/SubwayLines";
#if DEBUG
            TestObjectsLocation       = $"{CurrentDirectory}/TestObjects";
            TestCelestialBodyLocation = $"{TestObjectsLocation}/CelestialBodies";
            TestSubwayLineLocation    = $"{TestObjectsLocation}/SubwayLines";
#endif
        }

        private string settingsLocation;
        private string celestialBodyLocation;
        private string subwayLineLocation;
#if DEBUG
        private string testObjectsLocation;
        private string testCelestialBodyLocation;
        private string testSubwayLineLocation;
#endif

        /// <summary>
        /// Gets the current directory that this program is located in.
        /// </summary>
        [DataMember]
        public string CurrentDirectory { get; set; } = Environment.CurrentDirectory;

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

        /// <summary>
        /// Gets or sets the directory where the <see cref="SubwayLine"/> XML files are located.
        /// </summary>
        [DataMember]
        public string SubwayLineLocation
        {
            get { return this.subwayLineLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value == this.subwayLineLocation)
                {
                    return;
                }

                this.subwayLineLocation = value;

                OnPropertyChanged(nameof(SubwayLineLocation));
                DirectoryEx.CreateDirectoryIfNonexistent(SubwayLineLocation);
            }
        }

#if DEBUG
        /// <summary>
        /// Gets or sets the directory where various <see cref="CelestialBody"/> and
        /// <see cref="SubwayLine"/> XML files that are used in unit tests are located.
        /// </summary>
        public string TestObjectsLocation
        {
            get { return this.testObjectsLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value == this.testObjectsLocation)
                {
                    return;
                }

                this.testObjectsLocation = value;

                OnPropertyChanged(TestObjectsLocation);
                DirectoryEx.CreateDirectoryIfNonexistent(TestObjectsLocation);
            }
        }

        /// <summary>
        /// Gets or sets the directory where various <see cref="CelestialBody"/> 
        /// XML files that are used in <see langword="unit tests"/> are located.
        /// </summary>
        public string TestCelestialBodyLocation
        {
            get { return this.testCelestialBodyLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value == this.testCelestialBodyLocation)
                {
                    return;
                }

                this.testCelestialBodyLocation = value;

                OnPropertyChanged(TestCelestialBodyLocation);
                DirectoryEx.CreateDirectoryIfNonexistent(TestCelestialBodyLocation);
            }
        }

        /// <summary>
        /// Gets or sets the directory where various <see cref="SubwayLine"/> 
        /// XML files that are used in <see langword="unit tests"/> are located.
        /// </summary>
        public string TestSubwayLineLocation
        {
            get { return this.testSubwayLineLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value == this.testSubwayLineLocation)
                {
                    return;
                }

                this.testSubwayLineLocation = value;

                OnPropertyChanged(TestSubwayLineLocation);
                DirectoryEx.CreateDirectoryIfNonexistent(TestSubwayLineLocation);
            }
        }
#endif

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
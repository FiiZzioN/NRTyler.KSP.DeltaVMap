// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
//
// Author           : Nicholas Tyler
// Created          : 10-30-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 11-04-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataProviders
{
    public class SubwayLine : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubwayLine"/> class.
        /// </summary>
        /// <param name="target">The <see cref="CelestialBody"/> that this landing information is dedicated to.</param>
        public SubwayLine(CelestialBody target) : this(target, $"{target.Name.ToTitleCase()}Line")
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubwayLine"/> class.
        /// </summary>
        /// <param name="target">The <see cref="CelestialBody"/> that this landing information is dedicated to.</param>
        /// <param name="identifier">An identifier to facilitate easier sorting of other <see cref="SubwayLine"/>.</param>
        public SubwayLine(CelestialBody target, string identifier)
        {
            Target     = target;
            Identifier = String.IsNullOrWhiteSpace(identifier) ? "Invalid Identifier" : identifier;
        }

        private string identifier;
        private CelestialBody target;
        private SortedDictionary<int, SubwayStep> line;
        private LineInformation lineInformation;

        public string Identifier
        {
            get { return this.identifier; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                this.identifier = value;
                OnPropertyChanged(nameof(Identifier));
            }
        }

        public SortedDictionary<int, SubwayStep> Line
        {
            get
            {
                return this.line ?? (this.line = new SortedDictionary<int, SubwayStep>());
            }
            set
            {
                this.line = value; 
                OnPropertyChanged(nameof(Line));
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="CelestialBody"/> that this <see cref="SubwayLine"/> is dedicated to.
        /// </summary>
        public CelestialBody Target
        {
            get { return this.target; }
            set
            {
                this.target = value;
                OnPropertyChanged(nameof(Target));
            }
        }

        public LineInformation LineInformation
        {
            get
            {
                return this.lineInformation ?? (this.lineInformation = new LineInformation());
            }
            set
            {
                this.lineInformation = value; 
                OnPropertyChanged(nameof(LineInformation));
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
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
// 
// Author           : Nicholas Tyler
// Created          : 10-30-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-30-2017
// 
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.KSP.DeltaVMap.Core.Enums;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataProviders
{
    /// <summary>
    /// A base class that defines specific information about a given step on the <see cref="SubwayLine"/>.
    /// </summary>
    [Serializable]
    public class SubwayStep :INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubwayStep"/> class.
        /// </summary>
        protected SubwayStep() : this("Undefined")
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubwayStep"/> class.
        /// </summary>
        /// <param name="name">The name of this specific <see cref="SubwayStep"/>.</param>
        public SubwayStep(string name)
        {
            Name = name;
            Initialize();
        }

        #region Fields and Properties

        protected string name;
        protected StepID stepID;
        protected Dictionary<string, int> energyRequired;

        /// <summary>
        /// Gets or sets the name of this specific <see cref="SubwayStep"/>.
        /// </summary>
        protected string Name
        {
            get { return this.name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                this.name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Enums.StepID"/> that shows 
        /// where you're currently at on the <see cref="SubwayLine"/>.
        /// </summary>
        public virtual StepID StepID
        {
            get { return this.stepID; }
            set
            {
                if (StepID == value) return;

                this.stepID = value;
                OnPropertyChanged(nameof(StepID));
            }
        }

        /// <summary>
        /// Gets or sets the energy required to get to this step on the <see cref="SubwayLine"/>.
        /// </summary>
        public Dictionary<string, int> EnergyRequired
        {
            get { return this.energyRequired; }
            set
            {
                this.energyRequired = value;
                OnPropertyChanged(nameof(EnergyRequired));
            }
        }


        #endregion

        #region Methods

        /// <summary>
        /// Grants the ability to set all three possible energy required values at once. 
        /// </summary>
        /// <param name="minimum">The minimum amount of energy required to get to your destination.</param>
        /// <param name="average">The average amount of energy required to get to your destination.</param>
        /// <param name="maximum">The maximum amount of energy required to get to your destination.</param>
        public virtual void SetEnergyRequired(int minimum, int average, int maximum)
        {
            EnergyRequired["Minimum"] = minimum;
            EnergyRequired["Average"] = average;
            EnergyRequired["Maximum"] = maximum;
        }

        /// <summary>
        /// Should the energy required to reach a given destination not have a large deviation, there's no 
        /// need to have a range of values to display. While you enter in the average amount of energy required 
        /// to reach the destination, all other values are set to zero to represent this fact.
        /// </summary>
        /// <param name="average">The average amount of energy required to get to your destination.</param>
        public virtual void SetEnergyRequired(int average)
        {
            EnergyRequired["Minimum"] = 0;
            EnergyRequired["Average"] = average;
            EnergyRequired["Maximum"] = 0;
        }

        /// <summary>
        /// Initializes the "EnergyRequired" property with a Dictionary that already 
        /// includes the required "Minimum", "Average", and "Maximum" entries. 
        /// </summary>
        private void Initialize()
        {
            EnergyRequired = new Dictionary<string, int>
            {
                {"Minimum", 0},
                {"Average", 0},
                {"Maximum", 0},
            };
        }

        #endregion

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
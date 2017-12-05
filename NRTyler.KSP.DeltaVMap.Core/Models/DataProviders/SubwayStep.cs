// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
//
// Author           : Nicholas Tyler
// Created          : 10-30-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 11-26-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Utilities;
using NRTyler.KSP.DeltaVMap.Core.Enums;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataProviders
{
    /// <summary>
    /// A base class that defines specific information about a given step on the <see cref="SubwayLine"/>.
    /// </summary>
    [Serializable]
    [DataContract(Name = "SubwayStep")]
    public abstract class SubwayStep : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubwayStep" /> class.
        /// </summary>
        /// <param name="target">The <see cref="CelestialBody"/> that this information is dedicated to.</param>
        protected SubwayStep(CelestialBody target) : this(target, target.Name)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubwayStep" /> class.
        /// </summary>
        /// <param name="target">The <see cref="CelestialBody"/> that this information is dedicated to.</param>
        /// <param name="name">The name of this specific <see cref="SubwayStep" />.</param>
        protected SubwayStep(CelestialBody target, string name)
        {
            Initialize(target, name);
        }

        #region Fields and Properties

        private string name;
        private CelestialBody target;
        private StepID stepID;
        private Dictionary<string, double> energyRequired;

        /// <summary>
        /// Gets or sets the name of this specific <see cref="SubwayStep"/>.
        /// </summary>
        [DataMember]
        public virtual string Name
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
        /// Gets or sets the <see cref="CelestialBody"/> you're trying to get to.
        /// </summary>
        [DataMember]
        public virtual CelestialBody Target
        {
            get { return this.target; }
            set
            {
                // Pick your poison:

                // Faster, but could say they aren't equal because it isn't
                // the same instance even thought it has the same values.
                //if (Object.ReferenceEquals(this.target, value)) return;

                // Slower, but does a deep comparison to see if the values
                // are the same, regardless if they're the same instance.
                if (this.target.CompareObject(value)) return;

                this.target = value; 
                OnPropertyChanged(nameof(Target));
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Enums.StepID"/> that shows 
        /// where you're currently at on the <see cref="SubwayLine"/>.
        /// </summary>
        [DataMember]
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
        /// The three keys, spelt exactly as shown, are "Minimum", "Maximum", and "Average".
        /// </summary>
        [DataMember]
        public virtual Dictionary<string, double> EnergyRequired
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
        /// <param name="maximum">The maximum amount of energy required to get to your destination.</param>
        public virtual void SetEnergyRequired(double minimum, double maximum)
        {
            EnergyRequired["Minimum"] = minimum;
            EnergyRequired["Maximum"] = maximum;

            EnergyRequired["Average"] = GetAverage();

        }

        /// <summary>
        /// Should the energy required to reach a given destination not have a large deviation, there's no 
        /// need to have a range of values to display. While you enter in the amount of energy required 
        /// to reach the destination, all fields are set to the same value.
        /// </summary>
        /// <param name="energy">The amount of energy required to get to your destination.</param>
        public virtual void SetEnergyRequired(double energy)
        {
            EnergyRequired["Minimum"] = energy;
            EnergyRequired["Maximum"] = energy;

            EnergyRequired["Average"] = energy;
        }

        /// <summary>
        /// Gets the average amount of deltaV required to reach the targeted destination.
        /// </summary>
        /// <returns>The average amount of deltaV required to reach the targeted destination.</returns>
        protected virtual double GetAverage()
        {
            var minimum = EnergyRequired["Minimum"];
            var maximum = EnergyRequired["Maximum"];

            return ((minimum + maximum) / 2);
        }

        /// <summary>
        /// Initializes the class's 'Target' and 'Name' properties. This also instantiates the
        /// 'EnergyRequired' property with a Dictionary that already includes the required 
        /// "Minimum", "Average", and "Maximum" entries. 
        /// </summary>
        private void Initialize(CelestialBody target, string name)
        {
            Name   = String.IsNullOrWhiteSpace(name) ? "Invalid Name" : name;
            Target = target;

            EnergyRequired = new Dictionary<string, double>
            {
                {"Minimum", 0},
                {"Maximum", 0},
                {"Average", 0},
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
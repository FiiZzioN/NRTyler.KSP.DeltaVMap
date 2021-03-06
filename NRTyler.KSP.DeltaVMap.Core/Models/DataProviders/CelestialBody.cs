﻿// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
//
// Author           : Nicholas Tyler
// Created          : 10-14-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 11-04-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.KSP.DeltaVMap.Core.Enums;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataProviders
{
    /// <summary>
    /// Has fields that hold information that are unique to celestial bodies such as planets, moons 
    /// and their stars that can be used on a delta-v plot.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "CelestialBody")]
    public class CelestialBody : INotifyPropertyChanged//, IXmlSerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CelestialBody"/> class.
        /// </summary>
        public CelestialBody()
        {

        }

        #region Fields

        private string name;
        private BodyType bodyType;
        private bool isHomeWorld;
        private bool hasAtmosphere;
        private bool canUseJets;
        private bool hasOrbitingBodies;
        private int numberOfOrbitingBodies;
        private CelestialBody host;
        private List<CelestialBody> orbitingBodies;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of this <see cref="CelestialBody"/>.
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
        /// Gets or sets whether this <see cref="CelestialBody"/> is a Star, Planet, or Moon.
        /// </summary>
        [DataMember]
        public BodyType BodyType
        {
            get { return this.bodyType; }
            set
            {
                // If the body is a star, there are certain values that must be changed.
                if (value == BodyType.Star)
                {
                    SetValuesIfStar();
                }

                this.bodyType = value;
                OnPropertyChanged(nameof(BodyType));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CelestialBody"/> is where the player's primary 
        /// base of operations is located. In short, this is where you launch your rockets from.
        /// </summary>
        [DataMember]
        public virtual bool IsHomeWorld
        {
            get { return this.isHomeWorld; }
            set
            {
                this.isHomeWorld = value;
                OnPropertyChanged(nameof(IsHomeWorld));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CelestialBody"/> has an atmosphere.
        /// </summary>
        [DataMember]
        public virtual bool HasAtmosphere
        {
            get { return this.hasAtmosphere; }
            set
            {
                this.hasAtmosphere = value;
                OnPropertyChanged(nameof(HasAtmosphere));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CelestialBody"/> has an 
        /// atmosphere that also allows the use of jet engines.
        /// </summary>
        [DataMember]
        public virtual bool CanUseJets
        {
            get { return this.canUseJets; }
            set
            {
                // If you can use jets here, then there must be an atmosphere.
                if (value)
                {
                    HasAtmosphere = true;
                }

                this.canUseJets = value;
                OnPropertyChanged(nameof(CanUseJets));
            }
        }

        /// <summary>
        /// Gets a value indicating whether there are any other bodies that orbit this <see cref="CelestialBody"/>.
        /// </summary>
        [DataMember]
        public virtual bool HasOrbitingBodies
        {
            get
            {
                HasOrbitingBodies = OrbitingBodies.Any();
                return this.hasOrbitingBodies;
            }
            private set
            {
                this.hasOrbitingBodies = value;
                OnPropertyChanged(nameof(HasOrbitingBodies));
            }
        }

        /// <summary>
        /// Gets the number of other bodies that orbit this <see cref="CelestialBody"/>.
        /// </summary>
        [DataMember]
        public virtual int NumberOfOrbitingBodies
        {
            get
            {
                NumberOfOrbitingBodies = OrbitingBodies.Count;
                return this.numberOfOrbitingBodies;
            }
            private set
            {
                this.numberOfOrbitingBodies = value;
                OnPropertyChanged(nameof(NumberOfOrbitingBodies));
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="CelestialBody"/>, also known as a 
        /// star, that this <see cref="CelestialBody"/> is orbiting.
        /// </summary>
        //[DataMember]
        public virtual CelestialBody Host
        {
            get { return this.host; }
            set
            {
                // If 'value' isn't null, then we use it. 
                // Otherwise, we just use the already existing value.
                this.host = value ?? this.host;
                OnPropertyChanged(nameof(Host));
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="List{T}"/> of other bodies that orbit this <see cref="CelestialBody"/>.
        /// </summary>
        [DataMember]
        public virtual List<CelestialBody> OrbitingBodies
        {
            get
            {
                return this.orbitingBodies ?? (this.orbitingBodies = new List<CelestialBody>());
            }
            private set
            {
                this.orbitingBodies = value;

                UpdateOrbitingBodiesInfo();
                OnPropertyChanged(nameof(OrbitingBodies));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates other relevant items should the <see cref="List{T}"/> of orbiting bodies be modified.
        /// </summary>
        public void UpdateOrbitingBodiesInfo()
        {
            // If there are any bodies in the list, then this CelestialBody obviously has moon(s).
            // If it doesn't have any in the list, then it clearly has no moon(s).
            if (OrbitingBodies.Any())
            {
                HasOrbitingBodies = true;
            }

            if (!OrbitingBodies.Any())
            {
                HasOrbitingBodies = false;
            }

            // Make sure the number of moons has been updated. Since this method is called when 
            // the 'Moons' property has been modified.
            if (NumberOfOrbitingBodies != OrbitingBodies.Count)
            {
                NumberOfOrbitingBodies = OrbitingBodies.Count;
            }
        }

        /// <summary>
        /// Changes various values of this <see cref="CelestialBody"/> to make it a star, and modifies other 
        /// <see cref="CelestialBody"/>'s to recognize that change.
        /// </summary>
        public void SetValuesIfStar()
        {
            Host = this;
            HasAtmosphere = true;
            CanUseJets = false;
            IsHomeWorld = false;
            UpdateOrbitingBodiesInfo();
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when a property value changes.
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
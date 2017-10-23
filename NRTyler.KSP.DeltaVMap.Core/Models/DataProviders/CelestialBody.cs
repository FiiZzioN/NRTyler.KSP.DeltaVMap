// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
//
// Author           : Nicholas Tyler
// Created          : 10-14-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-22-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataProviders
{
    /// <summary>
    /// Has fields that hold information that are unique to celestial bodies such as planets, moons 
    /// and their stars that can be used on a delta-v plot.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    public class CelestialBody : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CelestialBody"/> class.
        /// </summary>
        public CelestialBody()
        {

        }

        #region Fields

        private string name;
        private bool isStar;
        private bool isPlanet;
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
        public virtual string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CelestialBody"/> is a star. If this 
        /// value is <see langword="false"/>, it should be assumed that this <see cref="CelestialBody"/> 
        /// is either a planet or a moon.
        /// </summary>
        public bool IsStar
        {
            get { return this.isStar; }
            set
            {
                // If this CelestialBody is a star, certain values must be changed.
                if (value)
                {
                    SetValuesIfStar();
                }

                this.isStar = value;
                OnPropertyChanged(nameof(IsStar));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CelestialBody"/> is a planet. If this value 
        /// is <see langword="false"/>, it should be assumed that this <see cref="CelestialBody"/> is a moon.
        /// </summary>
        public virtual bool IsPlanet
        {
            get { return this.isPlanet; }
            set
            {
                this.isPlanet = value; 
                OnPropertyChanged(nameof(IsPlanet));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CelestialBody"/> is where the player's primary 
        /// base of operations is located. In short, this is where you launch your rockets from.
        /// </summary>
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
        public virtual List<CelestialBody> OrbitingBodies
        {
            get
            {
                // Make sure we receive something.
                EnsureInstantiation();

                return this.orbitingBodies;
            }
            set
            {
                // Make sure we can modify something.
                EnsureInstantiation();

                this.orbitingBodies = value;

                ModifyRelevantItems();
                OnPropertyChanged(nameof(OrbitingBodies));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Makes sure that 'OrbitingBodies' has a <see cref="List{T}"/> instantiated before trying to access it.
        /// </summary>
        protected virtual void EnsureInstantiation()
        {
            if (OrbitingBodies == null)
            {
                OrbitingBodies = new List<CelestialBody>();
            }
        }

        /// <summary>
        /// Updates other relevant items should the <see cref="List{T}"/> of orbiting bodies be modified.
        /// </summary>
        protected virtual void ModifyRelevantItems()
        {
            // If there are any bodies in the list, then this CelestialBody obviously has moon(s).
            // If it doesn't have any in the list, then it clearly has no moon(s).
            if (OrbitingBodies.Any())
            {
                HasOrbitingBodies = true;
            }
            else if (!OrbitingBodies.Any())
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
        protected virtual void SetValuesIfStar()
        {
            Host = this;
            HasAtmosphere = true;
            HasOrbitingBodies = true;

            IsPlanet = false;
            CanUseJets = false;
            IsHomeWorld = false;

            #if !DEBUG
            throw new NotImplementedException
                ("You have to add all of the other Celestial Bodies to the moons list before continuing!");
            #endif

            if (OrbitingBodies.Count <= 0)
            {
                return;
            }
            foreach (var celestialBody in OrbitingBodies)
            {
                // Since this CelestialBody is now the star, it makes itself the host of every 
                // other CelestialBody an makes sure to set their "IsStar" flag is false.
                celestialBody.Host = this;
                celestialBody.IsStar = false;

                if (celestialBody.OrbitingBodies.Contains(this))
                {
                    celestialBody.OrbitingBodies.Remove(this);
                }
            }
        }

        /// <summary>
        /// Adds the specified celestialBodies to the <see cref="OrbitingBodies" /><see cref="List{T}" />.
        /// This is done using a user specified approvalMethod to ensure that only valid bodies that meet
        /// the required criteria can be added to the <see cref="List{T}" />.
        /// </summary>
        /// <param name="approvalMethod">
        /// The logic that determines whether a body can be added to the 
        /// <see cref="OrbitingBodies"/> <see cref="List{T}"/>
        /// </param>
        /// <param name="celestialBodies">The moons to add.</param>
        protected virtual void AddCelestialBodies(Predicate<CelestialBody[]> approvalMethod, params CelestialBody[] celestialBodies)
        {
            approvalMethod(celestialBodies);
        }

        /// <summary>
        /// Contains logic that only allows planets to be added 
        /// to the <see cref="OrbitingBodies"/> <see cref="List{T}"/>.
        /// </summary>
        /// <param name="celestialBodies">The celestial bodies to validate.</param>
        protected virtual void AddPlanets(CelestialBody[] celestialBodies)
        {
            // Only a star can have planets.
            if (!IsStar)
            {
                return;
            }

            foreach (var celestialBody in celestialBodies)
            {
                // Ensure it's a planet and duplicates aren't added.
                if (celestialBody.IsPlanet && !OrbitingBodies.Contains(celestialBody))
                {
                    OrbitingBodies.Add(celestialBody);
                }
            }
        }

        /// <summary>
        /// Contains logic that only allows moons to be added 
        /// to the <see cref="OrbitingBodies"/> <see cref="List{T}"/>.
        /// </summary>
        /// <param name="celestialBodies">The celestial bodies to validate.</param>
        protected virtual void AddMoons(CelestialBody[] celestialBodies)
        {
            // A star can't have moons.
            if (IsStar)
            {
                return;
            }

            foreach (var celestialBody in celestialBodies)
            {
                // Ensure it's a moon and duplicates aren't added.
                if (!celestialBody.IsPlanet && !OrbitingBodies.Contains(celestialBody))
                {
                    OrbitingBodies.Add(celestialBody);
                }
            }
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
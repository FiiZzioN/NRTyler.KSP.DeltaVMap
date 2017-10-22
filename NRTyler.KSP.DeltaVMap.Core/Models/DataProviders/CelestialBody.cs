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
        private bool hasMoons;
        private int numberOfMoons;
        private CelestialBody host;
        private List<CelestialBody> moons;

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
        /// Gets value indicating whether this <see cref="CelestialBody"/> has any moons.
        /// </summary>
        public virtual bool HasMoons
        {
            get
            {
                HasMoons = Moons.Any();
                return this.hasMoons;
            }
            private set
            {
                this.hasMoons = value;
                OnPropertyChanged(nameof(HasMoons));
            }
        }

        /// <summary>
        /// Gets the number of moons that orbit this <see cref="CelestialBody"/>.
        /// </summary>
        public virtual int NumberOfMoons
        {
            get
            {
                NumberOfMoons = Moons.Count;
                return this.numberOfMoons;
            }
            private set
            {
                this.numberOfMoons = value;
                OnPropertyChanged(nameof(NumberOfMoons));
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="CelestialBody"/> that this <see cref="CelestialBody"/> is orbiting.
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
        /// Gets or sets the <see cref="List{T}"/> of moons that are orbiting this <see cref="CelestialBody"/>.
        /// </summary>
        public virtual List<CelestialBody> Moons
        {
            get
            {
                EnsureMoonsInstantiation();
                return this.moons;
            }
            set
            {
                // Make sure we have something to add to.
                EnsureMoonsInstantiation();

                this.moons = value;

                ModifyMoonItems();
                OnPropertyChanged(nameof(Moons));
            }
        }

        #endregion

        /// <summary>
        /// Makes sure that 'Moons' has a <see cref="List{T}"/> instantiated before trying to access it.
        /// </summary>
        protected virtual void EnsureMoonsInstantiation()
        {
            // If 'moons' isn't null, then we just use the List that's already there.
            // Otherwise, we instantiate a new List for it to use.
            Moons = this.moons ?? new List<CelestialBody>();
        }

        /// <summary>
        /// Updates other items should the <see cref="List{T}"/> of moons be modified.
        /// </summary>
        protected virtual void ModifyMoonItems()
        {
            // Everything in here is pretty self-explanatory...
            if (Moons.Any())
            {
                HasMoons = true;
            }
            else if(!Moons.Any())
            {
                HasMoons = false;
            }

            if (NumberOfMoons != Moons.Count)
            {
                NumberOfMoons = Moons.Count;
            }
        }

        /// <summary>
        /// Changes various values of this <see cref="CelestialBody"/> to make it a star, and modifies other 
        /// <see cref="CelestialBody"/>'s to recognize that change.
        /// </summary>
        protected virtual void SetValuesIfStar()
        {
            Host          = this;
            HasMoons      = true;
            HasAtmosphere = true;

            IsPlanet    = false;
            CanUseJets  = false;
            IsHomeWorld = false;

            // Development Exception!
            throw new NotImplementedException
                ("You have to add all of the other Celestial Bodies to the moons list before continuing!");

            foreach (var celestialBody in Moons)
            {
                // Since this CelestialBody is now the star, it makes itself the host of every 
                // other CelestialBody an makes sure to set their "IsStar" flag is false.
                celestialBody.Host   = this;
                celestialBody.IsStar = false;

                if (celestialBody.Moons.Contains(this))
                {
                    celestialBody.Moons.Remove(this);
                }
            }
        }

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
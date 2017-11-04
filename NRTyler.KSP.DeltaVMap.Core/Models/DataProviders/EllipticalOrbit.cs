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
using System.ComponentModel;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataProviders
{
    /// <summary>
    /// The <see cref="SubwayStep"/> that signifies whether you're at the beginning of your journey, or whether you're returning home.
    /// </summary>
    /// <seealso cref="Orbit" />
    [Serializable]
    public class EllipticalOrbit : Orbit
    {
        public EllipticalOrbit()
        {
            
        }

        private bool canAeroBrake;
        private CelestialBody target;

        /// <summary>
        /// Gets or sets a value indicating whether you can use aerobraking to help 
        /// slow down enough to enter orbit around the targeted <see cref="CelestialBody"/>.
        /// </summary>
        public bool CanAeroBrake
        {
            get { return this.canAeroBrake; }
            set
            {
                this.canAeroBrake = value;
                OnPropertyChanged(nameof(CanAeroBrake));
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="CelestialBody"/> you're trying to get to.
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
    }
}
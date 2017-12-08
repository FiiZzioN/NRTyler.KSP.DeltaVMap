// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
//
// Author           : Nicholas Tyler
// Created          : 10-30-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-08-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using NRTyler.KSP.DeltaVMap.Core.Enums;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataProviders
{
    /// <summary>
    /// The <see cref="SubwayStep"/> that signifies whether you're at the beginning of your journey, or whether you're returning home.
    /// </summary>
    /// <seealso cref="Orbit" />
    [Serializable]
    public class EllipticalOrbit : Orbit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalOrbit"/> class.
        /// </summary>
        /// <param name="target">The <see cref="CelestialBody"/> that this information is dedicated to.</param>
        public EllipticalOrbit(CelestialBody target) : base(target, StepID.EllipticalOrbit)
        {
            CanAeroBrake = CheckForAtmosphere();
        }

        private bool canAeroBrake;

        /// <summary>
        /// Gets or sets a value indicating whether you can possibly use aerobraking to help 
        /// slow down enough to enter orbit around the targeted <see cref="CelestialBody"/>.
        /// </summary>
        public bool CanAeroBrake
        {
            get { return this.canAeroBrake; }
            private set
            {
                this.canAeroBrake = value;
                OnPropertyChanged(nameof(CanAeroBrake));
            }
        }
        
        /// <summary>
        /// Checks to see if the targeted <see cref="CelestialBody"/> has 
        /// an atmosphere that could possibly be used for aerobraking.
        /// </summary>
        /// <returns>
        /// Returns <see langword="true"/> if the targeted body has 
        /// an atmosphere, otherwise returns <see langword="false"/>.
        /// </returns>
        private bool CheckForAtmosphere()
        {
            return base.Target.HasAtmosphere;
        }
    }
}
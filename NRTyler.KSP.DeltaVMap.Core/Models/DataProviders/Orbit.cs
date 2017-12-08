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
using System.Collections.Generic;
using NRTyler.KSP.DeltaVMap.Core.Enums;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataProviders
{
    ///<inheritdoc cref="SubwayStep"/>
    /// <summary>
    /// A class that provides a base for any <see cref="SubwayStep"/> that involves an orbit or requires orbital parameters.
    /// </summary>
    /// <seealso cref="SubwayStep" />
    [Serializable]
    public class Orbit : SubwayStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Orbit" /> class.
        /// </summary>
        /// <param name="target">The <see cref="CelestialBody"/> that this information is dedicated to.</param>
        public Orbit(CelestialBody target) : base(target, StepID.Orbit)
        {
            Initialize();
        }

        /// <summary>
        /// Made specifically for the <see cref="EllipticalOrbit"/> class that inherits from <see cref="Orbit"/>.
        /// </summary>
        /// <param name="target">The <see cref="CelestialBody" /> that this information is dedicated to.</param>
        /// <param name="stepID">The <see cref="Enums.StepID"/> that represents what type of SubwayStep this is.</param>
        protected Orbit(CelestialBody target, StepID stepID) : base(target, stepID)
        {
            Initialize();
        }

        private Dictionary<string, int> orbitalParameters;
        private Dictionary<string, SubwayLine> detour;

        /// <summary>
        /// Gets or sets the orbital parameters that one should expect for after spending
        /// the amount of energy that's advertised on the <see cref="SubwayLine"/>
        /// </summary>
        public virtual Dictionary<string, int> OrbitalParameters
        {
            get { return this.orbitalParameters; }
            set
            {
                this.orbitalParameters = value;
                OnPropertyChanged(nameof(OrbitalParameters));
            }
        }

        /// <summary>
        /// Gets or sets the point where a <see cref="SubwayLine"/> splits to provide 
        /// access to one of the targeted <see cref="CelestialBody"/>'s moon(s).
        /// </summary>
        public virtual Dictionary<string, SubwayLine> Detour
        {
            get
            {
                return this.detour ?? (this.detour = new Dictionary<string, SubwayLine>());
            }
            set
            {
                this.detour = value;
                OnPropertyChanged(nameof(Detour));
            }
        }

        /// <summary>
        /// Grants the ability to set both the Apoapsis and Periapsis values at once. 
        /// </summary>
        /// <param name="apoapsis">Sets the orbit's apoapsis.</param>
        /// <param name="periapsis">Sets the orbit's periapsis.</param>
        public virtual void SetOrbitalParameters(int apoapsis, int periapsis)
        {
            OrbitalParameters["Apoapsis"]  = apoapsis;
            OrbitalParameters["Periapsis"] = periapsis;
        }

        /// <summary>
        /// Initializes the "OrbitalParameters" property with a Dictionary that 
        /// already includes the required "Apoapsis" and "Periapsis" entries. 
        /// </summary>
        private void Initialize()
        {
            OrbitalParameters = new Dictionary<string, int>
            {
                {"Apoapsis", 0},
                {"Periapsis", 0},
            };
        }
    }
}
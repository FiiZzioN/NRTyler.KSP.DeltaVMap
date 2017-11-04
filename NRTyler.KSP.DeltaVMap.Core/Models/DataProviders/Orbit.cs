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
        /// Initializes a new instance of the <see cref="Orbit"/> class.
        /// </summary>
        protected Orbit()
        {
            Initialize();
        }

        protected Dictionary<string, int> orbitalParameters;

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
        /// Grants the ability to set both the Apoapsis and Periapsis values at once. 
        /// </summary>
        /// <param name="apoapsis">The apoapsis.</param>
        /// <param name="periapsis">The periapsis.</param>
        public virtual void SetOrbitalParameters(int apoapsis, int periapsis)
        {
            OrbitalParameters["Apoapsis"] = apoapsis;
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
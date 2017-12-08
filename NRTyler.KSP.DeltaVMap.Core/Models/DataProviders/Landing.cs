// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
// 
// Author           : Nicholas Tyler
// Created          : 11-04-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 11-04-2017
// 
// License          : MIT License
// ***********************************************************************

using System;
using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.DeltaVMap.Core.Enums;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataProviders
{
    [Serializable]
    public class Landing : SubwayStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Landing"/> class.
        /// </summary>
        /// <param name="target">The <see cref="CelestialBody"/> that this landing information is meant for.</param>
        public Landing(CelestialBody target) : base(target, StepID.Landing)
        {
            CanUseParachutes = CheckForAtmosphere();
        }

        private bool canUseParachutes;

        /// <summary>
        /// Gets a value indicating whether this <see cref="CelestialBody"/> 
        /// has an atmosphere that facilitates the use of parachutes
        /// </summary>
        public bool CanUseParachutes
        {
            get { return this.canUseParachutes; }
            private set
            {
                this.canUseParachutes = value; 
                OnPropertyChanged(nameof(CanUseParachutes));
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
            return Target.HasAtmosphere;
        }
    }
}
// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
//
// Author           : Nicholas Tyler
// Created          : 10-30-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 11-04-2017
//
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.DeltaVMap.Core.Enums;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataProviders
{
    public class Intercept : SubwayStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intercept"/> class.
        /// </summary>
        /// <param name="target">The <see cref="CelestialBody"/> that this information is dedicated to.</param>
        public Intercept(CelestialBody target) : base(target)
        {
            StepID = StepID.Intercept;
        }

        private int timeUntilIntercept;

        /// <summary>
        /// Gets or sets the time until you'll intercept the targeted <see cref="CelestialBody"/>.
        /// </summary>
        public int TimeUntilIntercept
        {
            get { return this.timeUntilIntercept; }
            set
            {
                if (value < 0) return;
                
                this.timeUntilIntercept = value; 
                OnPropertyChanged(nameof(TimeUntilIntercept));
            }
        }
    }
}
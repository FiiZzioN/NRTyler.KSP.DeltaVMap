// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
// 
// Author           : Nicholas Tyler
// Created          : 11-16-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 11-16-2017
// 
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using NRTyler.KSP.DeltaVMap.Core.Enums;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataControllers
{
    /// <summary>
    /// Holds additional methods that aid in the creation or editing of a <see cref="CelestialBody"/>.
    /// </summary>
    public class BodyEditor
    {
        public BodyEditor(CelestialBody celestialBody)
        {
            BodyBeingEdited = celestialBody;
        }

        /// <summary>
        /// Gets the <see cref="CelestialBody"/> that's being edited.
        /// </summary>
        private CelestialBody BodyBeingEdited { get; }

        #region Methods

        /// <summary>
        /// Allows <see cref="CelestialBody"/> object(s) marked as a planet to be added to the body being 
        /// edited's "OrbitingBodies" list. If the body being edited isn't a star, the body won't be added, 
        /// as any <see cref="CelestialBody"/> that orbits a star is automatically considered a planet.
        /// </summary>
        /// <param name="celestialBodies">
        /// The <see cref="CelestialBody"/> object(s) you wish to add, delimited by a comma.
        /// </param>
        public void AddPlanets(params CelestialBody[] celestialBodies)
        {
            AddPlanets(celestialBodies.ToList());
        }

        /// <summary>
        /// Allows <see cref="CelestialBody"/> object(s) marked as a planet to be added to the body being 
        /// edited's "OrbitingBodies" list. If the body being edited isn't a star, the body won't be added, 
        /// as any <see cref="CelestialBody"/> that orbits a star is automatically considered a planet.
        /// </summary>
        /// <param name="celestialBodies">
        /// The collection of <see cref="CelestialBody"/> object(s) you wish to add.
        /// </param>
        public void AddPlanets(IEnumerable<CelestialBody> celestialBodies)
        {
            if (celestialBodies == null)
            {
                throw new ArgumentNullException(nameof(celestialBodies), "The collection of CelestialBodies cannot be null!");
            }

            // Only a star can have planets.
            if (BodyBeingEdited.BodyType != BodyType.Star) return;

            foreach (var celestialBody in celestialBodies)
            {
                // These values must be opposite of what they are now for the body being evaluated to pass.
                var isPlanet = false;
                var isDuplicate = true;

                // Ensure it's a planet AND that it's not already on the "OrbitingBodies" list.
                if (celestialBody.BodyType == BodyType.Planet) isPlanet = true;
                if (!BodyBeingEdited.OrbitingBodies.Contains(celestialBody)) isDuplicate = false;

                // Must be a planet AND not a duplicate.
                if (isPlanet && !isDuplicate)
                {
                    BodyBeingEdited.OrbitingBodies.Add(celestialBody);
                }
            }
        }

        /// <summary>
        /// Allows <see cref="CelestialBody"/> object(s) marked as a moon to be added to the body being
        /// edited's "OrbitingBodies" list. If the body being edited isn't a planet or another moon, the 
        /// <see cref="CelestialBody"/> won't be added. Only planets and other moons can have moons orbiting them. 
        /// </summary>
        /// <param name="celestialBodies">
        /// The <see cref="CelestialBody"/> object(s) you wish to add, delimited by a comma.
        /// </param>
        public void AddMoons(params CelestialBody[] celestialBodies)
        {
            AddMoons(celestialBodies.ToList());
        }

        /// <summary>
        /// Allows <see cref="CelestialBody"/> object(s) marked as a moon to be added to the body being
        /// edited's "OrbitingBodies" list. If the body being edited isn't a planet or another moon, the 
        /// <see cref="CelestialBody"/> won't be added. Only planets and other moons can have moons orbiting them. 
        /// </summary>
        /// <param name="celestialBodies">
        /// The collection of <see cref="CelestialBody"/> object(s) you wish to add.
        /// </param>
        public void AddMoons(IEnumerable<CelestialBody> celestialBodies)
        {
            if (celestialBodies == null)
            {
                throw new ArgumentNullException(nameof(celestialBodies), "The collection of CelestialBodies cannot be null!");
            }

            // A star can't have moons.
            if (BodyBeingEdited.BodyType == BodyType.Star) return;

            foreach (var celestialBody in celestialBodies)
            {
                // These values must be opposite of what they are now for the body being evaluated to pass.
                var isMoon = false;
                var isDuplicate = true;

                // Ensure it's a moon AND that it's not already on the "OrbitingBodies" list.
                if (celestialBody.BodyType == BodyType.Moon) isMoon = true;
                if (!BodyBeingEdited.OrbitingBodies.Contains(celestialBody)) isDuplicate = false;

                // Must be a moon AND not a duplicate.
                if (isMoon && !isDuplicate)
                {
                    BodyBeingEdited.OrbitingBodies.Add(celestialBody);
                }
            }
        }

        #endregion
    }
}
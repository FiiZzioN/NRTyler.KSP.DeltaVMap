// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core.Tests
// 
// Author           : Nicholas Tyler
// Created          : 11-08-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 11-08-2017
// 
// License          : MIT License
// ***********************************************************************

using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.DeltaVMap.Core.Models;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;
using NRTyler.KSP.DeltaVMap.Core.Repositories;

namespace NRTyler.KSP.DeltaVMap.Core.Tests
{
    /// <summary>
    /// A base class meant for <see langword="unit tests"/> that need a <see cref="Core.Models.DataProviders.CelestialBody"/>
    /// initialized so various tests have something to work with.
    /// </summary>
    public abstract class BodyInitializer
    {
        #region Test Initialization

        /// <summary>
        /// The method that gets called after each test has been completed.
        /// </summary>
        [TestInitialize]
        public virtual void SetupCelestialBody()
        {
            CelestialBody = CreateBody();
        }

        /// <summary>
        /// Creates a <see cref="Core.Models.DataProviders.CelestialBody"/> for any test that needs it.
        /// The body that's automatically created is a Kerbin analog. Doesn't have a host set by default.
        /// </summary>
        /// <returns>The created body.</returns>
        public virtual CelestialBody CreateBody()
        {
            // Load the settings, get the stream to the "Kerbin" XML file, and then deserialize it.

            var settings   = new ApplicationSettings();
            var stream     = File.OpenRead($"{settings.CelestialBodyLocation}/Kerbin.xml");                
            var repository = new CelestialBodyRepository();

            return repository.Deserialize(stream);
        }

        #endregion

        /// <summary>
        /// Gets or sets the <see cref="Core.Models.DataProviders.CelestialBody"/> that's available for any test that needs it.
        /// </summary>
        public virtual CelestialBody CelestialBody { get; set; }






        /*
        #region Test Initialization

        /// <summary>
        /// The method that gets called after each test has been completed.
        /// </summary>
        [TestInitialize]
        public virtual void SetupCelestialBody()
        {
            CelestialBody = CreateBody();
        }

        /// <summary>
        /// Creates a <see cref="Core.Models.DataProviders.CelestialBody"/> for any test that needs it.
        /// The body that's automatically created is a Kerbin analog, minus the moons.
        /// </summary>
        /// <returns>The created body.</returns>
        public virtual CelestialBody CreateBody()
        {
            var body = new CelestialBody()
            {
                Name = "Kerbin",
                BodyType = BodyType.Planet,
                IsHomeWorld = true,
                HasAtmosphere = true,
                CanUseJets = true,
            };

            return body;
        }

        #endregion

        /// <summary>
        /// Gets or sets the <see cref="Core.Models.DataProviders.CelestialBody"/> that's available for any test that needs it.
        /// </summary>
        public virtual CelestialBody CelestialBody { get; set; }
        */
    }
}
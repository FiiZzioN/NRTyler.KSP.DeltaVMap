// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core.Tests
//
// Author           : Nicholas Tyler
// Created          : 11-08-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-08-2017
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
    /// A base class meant for <see langword="unit tests"/> that need a <see cref="CelestialBody"/>
    /// such as a Star, Planet, or Moon initialized so various tests have something to work with.
    /// </summary>
    public abstract class BodyInitializer
    {
        #region Test Initialization

            #region Infrastructure

            protected ApplicationSettings Settings { get; set; } = new ApplicationSettings();
            protected CelestialBodyRepository Repository { get; set; } = new CelestialBodyRepository();

            #endregion

            #region Properties

            /// <summary>
            /// Gets or sets the star that various <see langword="unit tests"/> can use.
            /// </summary>
            protected virtual CelestialBody Star { get; set; }

            /// <summary>
            /// Gets or sets the planet that various <see langword="unit tests"/> can use.
            /// </summary>
            protected virtual CelestialBody Planet { get; set; }

            /// <summary>
            /// Gets or sets the moon that various <see langword="unit tests"/> can use.
            /// </summary>
            protected virtual CelestialBody Moon { get; set; }

            #endregion

            #region CelestialBody Creation Methods

            /// <summary>
            /// Creates the star that's used in the test initializer.
            /// </summary>
            /// <returns>A <see cref="CelestialBody"/> that's marked as a star.</returns>
            protected virtual CelestialBody CreateStar()
            {
                // Get the file stream to the "Kerbol" XML file, and then deserialize it.
                var stream = File.OpenRead($"{Settings.TestCelestialBodyLocation}/Kerbol.xml");

                return Repository.Deserialize(stream);
            }

            /// <summary>
            /// Creates the planet that's used in the test initializer.
            /// </summary>
            /// <returns>A <see cref="CelestialBody"/> that's marked as a planet.</returns>
            protected virtual CelestialBody CreatePlanet()
            {
                // Get the file stream to the "Kerbin" XML file, and then deserialize it.
                var stream = File.OpenRead($"{Settings.TestCelestialBodyLocation}/Kerbin.xml");

                return Repository.Deserialize(stream);
            }

            /// <summary>
            /// Creates the moon that's used in the test initializer.
            /// </summary>
            /// <returns>A <see cref="CelestialBody"/> that's marked as a moon.</returns>
            protected virtual CelestialBody CreateMoon()
            {
                // Get the file stream to the "Mun" XML file, and then deserialize it.
                var stream = File.OpenRead($"{Settings.TestCelestialBodyLocation}/Mun.xml");

                return Repository.Deserialize(stream);
            }

            #endregion

            #region Initializer

            /// <summary>
            /// The method that gets called after each test has been completed.
            /// </summary>
            [TestInitialize]
            public virtual void SetupCelestialBodies()
            {
                Star   = CreateStar();
                Planet = CreatePlanet();
                Moon   = CreateMoon();
            }

            #endregion

        #endregion
    }
}
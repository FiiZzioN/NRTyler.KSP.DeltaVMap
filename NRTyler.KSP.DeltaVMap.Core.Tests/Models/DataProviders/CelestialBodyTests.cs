// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core.Tests
// 
// Author           : Nicholas Tyler
// Created          : 10-27-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-27-2017
// 
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.DeltaVMap.Core.Enums;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Tests.Models.DataProviders
{
    [TestClass]
    public class CelestialBodyTests
    {
        #region Test Initialization

        [TestInitialize]
        public void SetupCelestialBodies()
        {
            Star   = CreateStar();
            Planet = CreatePlanet();
            Moon   = CreateMoon();

            SolarSystem = InstantiateSolarSystem();
        }

        public static CelestialBody CreateStar()
        {
            var star = new CelestialBody
            {
                Name = "Kerbol",
                BodyType = BodyType.Star,
                IsHomeWorld = false,
                HasAtmosphere = true,
                CanUseJets = false,
            };

            return star;
        }

        public static CelestialBody CreatePlanet()
        {
            var planet = new CelestialBody()
            {
                Name = "Kerbin",
                BodyType = BodyType.Planet,
                IsHomeWorld = true,
                HasAtmosphere = true,
                CanUseJets = true,            
            };

            return planet;
        }

        public static CelestialBody CreateMoon()
        {
            var moon = new CelestialBody()
            {
                Name = "Mun",
                BodyType = BodyType.Moon,
                IsHomeWorld = false,
                HasAtmosphere = false,
                CanUseJets = false,
            };

            return moon;
        }

        public static List<CelestialBody> InstantiateSolarSystem()
        {
            return new List<CelestialBody>()
            {
                Star,
                Planet,
                Moon
            };
        }

        #endregion

        public static List<CelestialBody> SolarSystem { get; set; }
        public static CelestialBody Star { get; set; }
        public static CelestialBody Planet { get; set; }
        public static CelestialBody Moon { get; set; }


        [TestMethod]
        public void NameTesting()
        {
            // Since this name isn't null, empty, or whitespace, it should be valid.
            Planet.Name = "MyPlanetName";
            Assert.AreEqual("MyPlanetName", Planet.Name);

            // Since this string is empty, it means that it's invalid. So we 
            // should expect the name we applied before to be returned.
            Planet.Name     = String.Empty;
            Assert.AreEqual("MyPlanetName", Planet.Name);
        }

        [TestMethod]
        public void BodyTypeTests()
        {
            // Changing the type to a moon.
            Planet.BodyType = BodyType.Moon; ;
            Assert.AreEqual(BodyType.Moon, Planet.BodyType);

            // Now lets see what happens when we change a moon to a star...
            Moon.BodyType = BodyType.Star;
            
            // We expect it to be a star, and since it's a star, it has an atmosphere.
            // And since it's the star, it has no official host, so it hosts itself.
            Assert.AreEqual(BodyType.Star, Moon.BodyType);
            Assert.IsTrue(Moon.HasAtmosphere);
            Assert.AreEqual(Moon, Moon.Host);
        }

        [TestMethod]
        public void HomeWorldChange()
        {
            // This was our home world...
            Planet.IsHomeWorld = false;
            Assert.IsFalse(Planet.IsHomeWorld);

            // Now this is our humble abode.
            Moon.IsHomeWorld = true;
            Assert.IsTrue(Moon.IsHomeWorld);

            // Our suits have incredible insulation!
            Star.IsHomeWorld = true;
            Assert.IsTrue(Star.IsHomeWorld);
        }

        [TestMethod]
        public void AtmosphereChange()
        {
            // We can't breathe anymore!
            Planet.HasAtmosphere = false;
            Assert.IsFalse(Planet.HasAtmosphere);

            // Since when did the moon get an atmosphere?!
            Moon.HasAtmosphere = true;
            Assert.IsTrue(Moon.HasAtmosphere);

            // Somehow this star has no atmosphere...
            Star.HasAtmosphere = false;
            Assert.IsFalse(Star.HasAtmosphere);
        }


        [TestMethod]
        public void JetChanges()
        {
            // Since we can now use jets on the moon, that means it has an atmosphere.
            Moon.CanUseJets = true;
            Assert.IsTrue(Moon.CanUseJets);
            Assert.IsTrue(Moon.HasAtmosphere);

            // We can't use jets anymore, but that doesn't mean it doesn't have an atmosphere.
            Planet.CanUseJets = false;
            Assert.IsFalse(Planet.CanUseJets);
            Assert.IsTrue(Planet.HasAtmosphere);
        }


        [TestMethod]
        public void AddPlanetsBasicTest()
        {
            // A star can only have planets, not moons. This means we expect only 
            // one object orbiting it and that it shows it has an orbiting body.
            Star.AddPlanets(Planet, Moon);
            Assert.IsTrue(Star.HasOrbitingBodies);
            Assert.AreEqual(1, Star.NumberOfOrbitingBodies);
            CollectionAssert.Contains(Star.OrbitingBodies, Planet);
        }

        [TestMethod]
        public void AddPlanetsDuplicateTest()
        {
            // A star can only have planets, not moons. This means we expect only 
            // one object orbiting it and that it shows it has an orbiting body.
            // There should only be one planet since the others are duplicates.
            Star.AddPlanets(Planet, Moon, Planet, Planet);
            Assert.IsTrue(Star.HasOrbitingBodies);
            Assert.AreEqual(1, Star.NumberOfOrbitingBodies);
            CollectionAssert.Contains(Star.OrbitingBodies, Planet);
        }

        [TestMethod]
        public void AddMoonsBasicTest()
        {
            // A planet can only have moons, not stars or other planets. This means we 
            // expect only one object orbiting it and that it shows it has an orbiting body.
            Planet.AddMoons(Moon, Star, Planet);
            Assert.IsTrue(Planet.HasOrbitingBodies);
            Assert.AreEqual(1, Planet.NumberOfOrbitingBodies);
            CollectionAssert.Contains(Planet.OrbitingBodies, Moon);
        }

        [TestMethod]
        public void AddMoonsDuplicateTest()
        {
            // A planet can only have moons, not stars or other planets. This means we 
            // expect only one object orbiting it and that it shows it has an orbiting body.
            // There should only be one moon since the others are duplicates.
            Planet.AddMoons(Moon, Star, Moon, Planet);
            Assert.IsTrue(Planet.HasOrbitingBodies);
            Assert.AreEqual(1, Planet.NumberOfOrbitingBodies);
            CollectionAssert.Contains(Planet.OrbitingBodies, Moon);
        }

        [TestMethod]
        public void AssignHosts()
        {
            // The star has no host, so it hosts itself. The planet orbits the star, so the star 
            // is its host body, and the moon orbits the planet, so the planet is its host body.
            Star.Host = Star;
            Planet.Host = Star;
            Moon.Host = Planet;

            Assert.AreEqual(Star, Star.Host);
            Assert.AreEqual(Star, Planet.Host);
            Assert.AreEqual(Planet, Moon.Host);
        }


    }
}
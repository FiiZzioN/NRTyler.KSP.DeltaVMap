// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core.Tests
//
// Author           : Nicholas Tyler
// Created          : 10-27-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 11-16-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.DeltaVMap.Core.Enums;

namespace NRTyler.KSP.DeltaVMap.Core.Tests.Models.DataProviders
{
    [TestClass]
    public class CelestialBodyTests : BodyInitializer
    {
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
// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core.Tests
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.DeltaVMap.Core.Models.DataControllers;

namespace NRTyler.KSP.DeltaVMap.Core.Tests.Models.DataControllers
{
    [TestClass]
    public class BodyEditorTests : BodyInitializer
    {
        [TestMethod]
        public void AddPlanetsBasicTest()
        {
            // A star can only have planets, not moons. This means we expect only 
            // one object orbiting it and that it shows it has an orbiting body.
            var editor = new BodyEditor(Star);

            editor.AddPlanets(Planet, Moon);
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
            var editor = new BodyEditor(Star);

            editor.AddPlanets(Planet, Moon, Planet, Planet);
            Assert.IsTrue(Star.HasOrbitingBodies);
            Assert.AreEqual(1, Star.NumberOfOrbitingBodies);
            CollectionAssert.Contains(Star.OrbitingBodies, Planet);
        }

        [TestMethod]
        public void AddMoonsBasicTest()
        {
            // A planet can only have moons, not stars or other planets. This means we 
            // expect only one object orbiting it and that it shows it has an orbiting body.
            var editor = new BodyEditor(Planet);

            editor.AddMoons(Moon, Star, Planet);
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
            var editor = new BodyEditor(Planet);

            editor.AddMoons(Moon, Star, Moon, Planet);
            Assert.IsTrue(Planet.HasOrbitingBodies);
            Assert.AreEqual(1, Planet.NumberOfOrbitingBodies);
            CollectionAssert.Contains(Planet.OrbitingBodies, Moon);
        }
    }
}
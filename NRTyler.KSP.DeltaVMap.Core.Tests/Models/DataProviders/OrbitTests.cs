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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.CodeLibrary.Utilities;
using NRTyler.KSP.DeltaVMap.Core.Enums;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Tests.Models.DataProviders
{
    [TestClass]
    public class OrbitTests : BodyInitializer
    {
        [TestMethod]
        public void OrbitInitialization()
        {
            var orbit = new Orbit(Planet);

            // Assert that the target objects are the same;
            Assert.IsTrue(Planet.CompareObject(orbit.Target));

            // Assert that the 'StepName' is correct.
            Assert.AreEqual("Kerbin Orbit", orbit.StepName);

            // Assert that the 'StepID' is 'Orbit' since this is the Orbit class.
            Assert.AreEqual(StepID.Orbit, orbit.StepID);

            // Assert that the 'OrbitalParameters' dictionary has been instantiated.
            Assert.AreEqual(0, orbit.OrbitalParameters["Apoapsis"]);
            Assert.AreEqual(0, orbit.OrbitalParameters["Periapsis"]);
        }

        [TestMethod]
        public void OrbitParameterChange()
        {
            var orbit = new Orbit(Moon);
            orbit.SetOrbitalParameters(140, 139);

            // The Apoapsis should be 140 while the Periapsis should be 139 respectively.
            Assert.AreEqual(140, orbit.OrbitalParameters["Apoapsis"]);
            Assert.AreEqual(139, orbit.OrbitalParameters["Periapsis"]);
        }

        //[TestMethod]
        public void OrbitDetourTest()
        {

        }
    }
}
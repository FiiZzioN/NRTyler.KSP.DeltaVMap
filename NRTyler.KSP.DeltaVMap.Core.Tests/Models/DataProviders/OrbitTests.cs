// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core.Tests
//
// Author           : Nicholas Tyler
// Created          : 11-08-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 11-16-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.DeltaVMap.Core.Enums;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Tests.Models.DataProviders
{
    [TestClass]
    public class OrbitTests : BodyInitializer
    {
        [TestMethod]
        public void OrbitCreationTests()
        {
            var orbit = new Orbit(Planet);

            // The steps name should be "Orbit" since it's an Orbit.
            // This is the orbit class, so the StepID should be "Orbit".
            // The orbital parameters should be zero since they haven't been set yet.

            Assert.AreEqual("Orbit", orbit.Name);
            Assert.AreEqual(StepID.Orbit, orbit.StepID);
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
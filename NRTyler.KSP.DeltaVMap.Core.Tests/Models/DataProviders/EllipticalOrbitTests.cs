// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core.Tests
// 
// Author           : Nicholas Tyler
// Created          : 12-08-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-08-2017
// 
// License          : MIT License
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.CodeLibrary.Utilities;
using NRTyler.KSP.DeltaVMap.Core.Enums;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Tests.Models.DataProviders
{
    [TestClass]
    public class EllipticalOrbitTests : BodyInitializer
    {
        [TestMethod]
        public void EllipticalOrbitInitialization()
        {
            var ellipticalOrbit = new EllipticalOrbit(Star);

            // Assert that the target objects are the same;
            Assert.IsTrue(Star.CompareObject(ellipticalOrbit.Target));

            // Assert that the 'StepName' is correct.
            Assert.AreEqual("Kerbol Elliptical Orbit", ellipticalOrbit.StepName);

            // Assert that the 'StepID' is 'EllipticalOrbit' since this is the EllipticalOrbit class.
            Assert.AreEqual(StepID.EllipticalOrbit, ellipticalOrbit.StepID);

            // Assert that the 'elliptialOrbitalParameters' dictionary has been instantiated.
            Assert.AreEqual(0, ellipticalOrbit.OrbitalParameters["Apoapsis"]);
            Assert.AreEqual(0, ellipticalOrbit.OrbitalParameters["Periapsis"]);

            // Assert that we can AeroBrake since the star has an atmosphere. We might get a bit toasty!
            Assert.IsTrue(ellipticalOrbit.CanAeroBrake);
        }

        [TestMethod]
        public void EllipticalOrbitParameterChange()
        {
            var ellipticalOrbit = new EllipticalOrbit(Planet);
            ellipticalOrbit.SetOrbitalParameters(240, 182);

            // The Apoapsis should be 240 while the Periapsis should be 182 respectively.
            Assert.AreEqual(240, ellipticalOrbit.OrbitalParameters["Apoapsis"]);
            Assert.AreEqual(182, ellipticalOrbit.OrbitalParameters["Periapsis"]);
        }

    }
}
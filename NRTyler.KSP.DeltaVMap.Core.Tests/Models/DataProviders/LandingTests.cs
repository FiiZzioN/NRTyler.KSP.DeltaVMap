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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.DeltaVMap.Core.Enums;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Tests.Models.DataProviders
{
    [TestClass]
    public class LandingTests : BodyInitializer
    {
        [TestMethod]
        public void LandingCreationTests()
        {
            var landing = new Landing(CelestialBody);

            // The steps name should be "Landing" since it's the landing.
            // This is the landing class, so the StepID should be "Landing".
            // We should be able to use chutes since Kerbin has an atmosphere.

            Assert.AreEqual("Landing", landing.Name);
            Assert.AreEqual(StepID.Landing, landing.StepID);
            Assert.IsTrue(landing.CanUseParachutes);

            CelestialBody.BodyType = BodyType.Moon;
        }
    }
}
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.CodeLibrary.Utilities;
using NRTyler.KSP.DeltaVMap.Core.Enums;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Tests.Models.DataProviders
{
    [TestClass]
    public class LandingTests : BodyInitializer
    {
        [TestMethod]
        public void LandingInitialization()
        {
            var landing = new Landing(Planet);

            // Assert that the target objects are the same;
            Assert.IsTrue(Planet.CompareObject(landing.Target));

            // Assert that the 'StepName' is correct.
            Assert.AreEqual($"{Planet.Name} Landing", landing.StepName);

            // Assert that the 'StepID' is 'Landing' since this is the landing class.
            Assert.AreEqual(StepID.Landing, landing.StepID);

            // Since 'Planet' has an atmosphere, we should be able to use chutes.
            Assert.IsTrue(landing.CanUseParachutes);
        }
    }
}
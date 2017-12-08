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
    public class InterceptTests : BodyInitializer
    {
        [TestMethod]
        public void InterceptInitialization()
        {
            // Create the object.
            var intercept = new Intercept(Moon);


            // Assert that the 'StepName' is correct.
            Assert.AreEqual($"{Moon.Name} Intercept", intercept.StepName);

            // Assert that the target objects are the same;
            Assert.IsTrue(Moon.CompareObject(intercept.Target));

            // Assert that the 'StepID' is 'Intercept' since this is the intercept class.
            Assert.AreEqual(StepID.Intercept, intercept.StepID);
        }

        [TestMethod]
        public void TimeUntilInterceptTests()
        {
            // Create the object.
            var intercept = new Intercept(Moon);

            // Test various 'TimeUntilIntercept' inputs.
            // Valid input:
            intercept.TimeUntilIntercept = 36;
            Assert.AreEqual(36, intercept.TimeUntilIntercept);

            // Invalid input:
            // Since I have the 'TimeUntilIntercept' property ignore values lower 
            // than '0', the time shouldn't have changed from the last input of '36'.
            intercept.TimeUntilIntercept = -8;
            Assert.AreEqual(36, intercept.TimeUntilIntercept);
        }
    }
}
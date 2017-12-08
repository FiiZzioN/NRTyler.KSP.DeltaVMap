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
using NRTyler.KSP.DeltaVMap.Core.Tests.DummyObjects;

namespace NRTyler.KSP.DeltaVMap.Core.Tests.Models.DataProviders
{
    [TestClass]
    public class SubwayStepTests : BodyInitializer
    {
        [TestMethod]
        public void SubwayStep_ValidConstructors()
        {
            // Get two objects, each using a different constructor.
            var dummyOne = new SubwayStepDummy(Planet, StepID.Orbit);
            var dummyTwo = new SubwayStepDummy(Moon, StepID.Intercept);
            
            // Check the first dummy's name and target to see if the assignment is correct.
            Assert.AreEqual($"{Planet.Name} Orbit", dummyOne.StepName);
            Assert.IsTrue(dummyOne.Target.CompareObject(Planet));

            // Check the second dummy's name and target to see if the assignment is correct.
            Assert.AreEqual($"{Moon.Name} Intercept", dummyTwo.StepName);
            Assert.IsTrue(dummyTwo.Target.CompareObject(Moon));
        }

        [TestMethod]
        public void SubwayStep_StepID()
        {
            // Set the two objects 'StepID' properties.
            var dummyOne = new SubwayStepDummy(Planet, StepID.Intercept);
            var dummyTwo = new SubwayStepDummy(Star, StepID.EllipticalOrbit);

            // Check the first dummy's StepID.
            Assert.AreEqual(StepID.Intercept, dummyOne.StepID);

            // Check the second dummy's StepID.
            Assert.AreEqual(StepID.EllipticalOrbit, dummyTwo.StepID);
        }

        [TestMethod]
        public void SubwayStep_SetEnergyMinMax()
        {
            // Set up the object and its StepID.
            var dummyOne = new SubwayStepDummy(Moon, StepID.Intercept);

            // Fields for easier asserts since I don't have to type each value multiple times.
            double min;
            double max;
            double ave;

            // Set fields for first Test.
            min = 1580;
            max = 3755;
            ave = 2667.5;

            // Call the method and then call the various asserts to check for the correct values.
            dummyOne.SetEnergyRequired(min, max);

            Assert.AreEqual(dummyOne.EnergyRequired["Minimum"], min);
            Assert.AreEqual(dummyOne.EnergyRequired["Maximum"], max);
            Assert.AreEqual(dummyOne.EnergyRequired["Average"], ave);

            // Set fields for second Test.
            min = 520.23;
            max = 906.47;
            ave = 713.35;

            // Call the method and then call the various asserts to check for the correct values.
            dummyOne.SetEnergyRequired(min, max);

            Assert.AreEqual(dummyOne.EnergyRequired["Minimum"], min);
            Assert.AreEqual(dummyOne.EnergyRequired["Maximum"], max);
            Assert.AreEqual(dummyOne.EnergyRequired["Average"], ave);

            // Set fields for third Test.
            min = 5231.82;
            max = 6550;
            ave = 5890.91;

            // Call the method and then call the various asserts to check for the correct values.
            dummyOne.SetEnergyRequired(min, max);

            Assert.AreEqual(dummyOne.EnergyRequired["Minimum"], min);
            Assert.AreEqual(dummyOne.EnergyRequired["Maximum"], max);
            Assert.AreEqual(dummyOne.EnergyRequired["Average"], ave);
        }

        [TestMethod]
        public void SubwayStep_SetEnergyOneValue()
        {
            // Set up the object and its StepID.
            var dummyOne = new SubwayStepDummy(Star, StepID.Orbit);

            // Field for easier asserts since I don't have to type the value multiple times.
            var value = 9350;

            // Call the method and then call the various asserts to check for the correct values.
            dummyOne.SetEnergyRequired(value);

            Assert.AreEqual(dummyOne.EnergyRequired["Minimum"], value);
            Assert.AreEqual(dummyOne.EnergyRequired["Maximum"], value);
            Assert.AreEqual(dummyOne.EnergyRequired["Average"], value);
        }
    }
}
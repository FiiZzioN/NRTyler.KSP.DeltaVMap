// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core.Tests
// 
// Author           : Nicholas Tyler
// Created          : 12-04-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-04-2017
// 
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Tests.DummyObjects
{
    /// <summary>
    /// A dummy <see cref="object"/> meant for testing the 
    /// <see langword="abstract"/> <see cref="SubwayStep"/> class. 
    /// </summary> 
    /// <seealso cref="NRTyler.KSP.DeltaVMap.Core.Models.DataProviders.SubwayStep" />
    public class SubwayStepDummy : SubwayStep 
    {
        public SubwayStepDummy(CelestialBody target) : this(target, target.Name)
        {

        }

        public SubwayStepDummy(CelestialBody target, string name) : base(target, name)
        {

        }
    }
}
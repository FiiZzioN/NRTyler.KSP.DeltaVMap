// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
// 
// Author           : Nicholas Tyler
// Created          : 10-13-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-13-2017
// 
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Attributes;

namespace NRTyler.KSP.DeltaVMap.Core.Enums
{
    public enum StepID
    {
        [StringLabel("Landing")]
        Landing = 0,

        [StringLabel("Low Orbit")]
        LowOrbit = 1,        

        [StringLabel("Elliptical Capture")]
        EllipticalCapture = 2,

        [StringLabel("Intercept")]
        Intercept = 3,
    }
}
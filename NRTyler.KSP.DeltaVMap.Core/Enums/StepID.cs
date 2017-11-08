// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
//
// Author           : Nicholas Tyler
// Created          : 10-13-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-30-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using NRTyler.CodeLibrary.Attributes;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Enums
{
    /// <summary>
    /// An <see cref="Enum"/> holding various ID's that show where you're currently at on the <see cref="SubwayLine"/>.
    /// </summary>
    public enum StepID
    {
        [StringLabel("Landing")]
        Landing = 0,

        [StringLabel("Orbit")]
        Orbit = 1,        

        [StringLabel("Elliptical Orbit")]
        EllipticalOrbit = 2,

        [StringLabel("Intercept")]
        Intercept = 3,
    }
}
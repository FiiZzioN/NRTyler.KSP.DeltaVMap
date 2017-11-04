// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
// 
// Author           : Nicholas Tyler
// Created          : 10-31-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-31-2017
// 
// License          : MIT License
// ***********************************************************************

using System;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Enums
{
    /// <summary>
    /// An <see cref="Enum"/> holding the various types a <see cref="CelestialBody"/> can be.
    /// </summary>
    public enum BodyType
    {
        Star = 0,
        Planet = 1,
        Moon = 2,
    }
}
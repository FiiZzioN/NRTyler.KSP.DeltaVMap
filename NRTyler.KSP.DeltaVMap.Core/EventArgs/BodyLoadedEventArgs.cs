// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
// 
// Author           : Nicholas Tyler
// Created          : 10-27-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-27-2017
// 
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.EventArgs
{
    public class BodyLoadedEventArgs
    {
        public BodyLoadedEventArgs(CelestialBody celestialBody)
        {
            CelestialBody = celestialBody;
        }

        public CelestialBody CelestialBody { get; set; }
    }
}
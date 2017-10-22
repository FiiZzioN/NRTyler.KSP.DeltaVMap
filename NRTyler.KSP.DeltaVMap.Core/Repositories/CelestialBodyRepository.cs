// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
// 
// Author           : Nicholas Tyler
// Created          : 10-14-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-14-2017
// 
// License          : MIT License
// ***********************************************************************

using System.IO;
using NRTyler.CodeLibrary.Interfaces.Generic;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Repositories
{
    public class CelestialBodyRepository : IRepository<CelestialBody>
    {
        /// <summary>
        /// Serializes the object to a file in binary format using the specified <see cref="T:System.IO.Stream" />.
        /// </summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream" /> to the specified location and mode.</param>
        /// <param name="obj">The <see cref="T:System.Object" /> to be serialized.</param>
        public void Serialize(Stream stream, CelestialBody obj)
        {
            
        }

        /// <summary>
        /// Deserializes a file saved in binary format using the specified <see cref="T:System.IO.Stream" />.
        /// </summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream" /> to the specified file and mode.</param>
        /// <returns>The deserialized object.</returns>
        public CelestialBody Deserialize(Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}
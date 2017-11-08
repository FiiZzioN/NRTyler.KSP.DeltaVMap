// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
//
// Author           : Nicholas Tyler
// Created          : 10-14-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 11-04-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using NRTyler.CodeLibrary.Utilities;
using NRTyler.KSP.DeltaVMap.Core.EventArgs;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;

namespace NRTyler.KSP.DeltaVMap.Core.Repositories
{
    public class CelestialBodyRepository : DataContractXmlRepository<CelestialBody>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CelestialBodyRepository"/> class.
        /// </summary>
        public CelestialBodyRepository()
        {
            
        }

        /// <summary>
        /// Serializes the <see cref="T:System.Object" /> to a file using the specified <see cref="T:System.IO.Stream" />.
        /// </summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream" /> to the specified location and what <see cref="T:System.IO.FileMode" /> it's using.</param>
        /// <param name="obj">The <see cref="T:System.Object" /> being serialized.</param>
        /// <exception cref="ArgumentNullException">stream - The stream being used can't be null!</exception>
        /// <exception cref="ArgumentNullException">obj - The object being serialized can't be null!</exception>
        public override void Serialize(Stream stream, CelestialBody obj)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream), "The stream being used can't be null!");
            if (obj == null) throw new ArgumentNullException(nameof(obj), "The object being serialized can't be null!");

            WriterSettings.Indent = true;
            WriterSettings.NewLineOnAttributes = true;

            Writer = XmlWriter.Create(stream, WriterSettings);

            using (Writer)
            {
                try
                {
                    DCSerializer.WriteObject(Writer, obj);
                }
                catch (InvalidDataContractException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                catch (SerializationException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// Deserializes a file using the specified <see cref="Stream" />.
        /// </summary>
        /// <param name="stream">The <see cref="Stream" /> to the specified file and what <see cref="FileMode" /> it's using.</param>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="ArgumentNullException">The stream being used can't be null!</exception>
        public override CelestialBody Deserialize(Stream stream)
        {
            return (CelestialBody)DCSerializer.ReadObject(stream);
        }

        public event EventHandler<BodyLoadedEventArgs> BodyLoaded;

        private void OnBodyLoaded(CelestialBody celestialBody)
        {
            BodyLoaded?.Invoke(this, new BodyLoadedEventArgs(celestialBody));
        }        
    }
}
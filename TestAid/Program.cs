// ***********************************************************************
// Assembly         : TestAid
//
// Author           : Nicholas Tyler
// Created          : 10-27-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 11-04-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.IO;
using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.DeltaVMap.Core.Enums;
using NRTyler.KSP.DeltaVMap.Core.Models;
using NRTyler.KSP.DeltaVMap.Core.Models.DataControllers;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;
using NRTyler.KSP.DeltaVMap.Core.Repositories;

namespace TestAid
{
    public class Program
    {
        private static void Main()
        {
            //var body = Generate();

            //var settings = new ApplicationSettings();
            //var stream = File.OpenWrite($"{settings.CelestialBodyLocation}/{body.Name.ToTitleCase()}.xml");
            //var repository = new CelestialBodyRepository();

            //repository.Serialize(stream, body);
            //Console.WriteLine("Done");
        }

        private static CelestialBody Generate()
        {
            var kerbol = new CelestialBody
            {
                Name = "Kerbol",
                BodyType = BodyType.Star,
                IsHomeWorld = false,
                HasAtmosphere = true,
                CanUseJets = false,
            };

            var kerbin = new CelestialBody
            {
                Name          = "Kerbin",
                BodyType      = BodyType.Planet,
                IsHomeWorld   = true,
                HasAtmosphere = true,
                CanUseJets    = true,
            };

            var mun = new CelestialBody()
            {
                Name          = "Mun",
                BodyType      = BodyType.Moon,
                IsHomeWorld   = false,
                HasAtmosphere = false,
                CanUseJets    = false,
            };

            var minmus = new CelestialBody()
            {
                Name          = "Minmus",
                BodyType      = BodyType.Moon,
                IsHomeWorld   = false,
                HasAtmosphere = false,
                CanUseJets    = false,
            };

            //var bodyEditorKerbin = new BodyEditor(kerbin);
            //var bodyEditorKerbol = new BodyEditor(kerbol);

            //bodyEditorKerbin.AddMoons(mun, minmus);
            //bodyEditorKerbol.AddPlanets(kerbin);

            return minmus;
        }
    }
}

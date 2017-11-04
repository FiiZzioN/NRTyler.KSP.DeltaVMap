using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.DeltaVMap.Core.Enums;
using NRTyler.KSP.DeltaVMap.Core.Models;
using NRTyler.KSP.DeltaVMap.Core.Models.DataProviders;
using NRTyler.KSP.DeltaVMap.Core.Repositories;

namespace TestAid
{
    public class Program
    {
        private static void Main()
        {
            var settings = new ApplicationSettings();
            var APPrepo  = new ApplicationSettingsRepository(settings);

            var appPath       = $"{settings.SettingsLocation}/Settings.xml";
            var appFileStream = File.OpenRead(appPath);

            Generation(settings);
            
            //Console.WriteLine(APPrepo.Deserialize(appFileStream).CelestialBodyLocation);
        }

        private static void Generation(ApplicationSettings settings)
        {
            var star = new CelestialBody
            {
                Name = "kerbol",
                BodyType = BodyType.Star,
                IsHomeWorld = false,
                HasAtmosphere = true,
                CanUseJets = false,
            };

            var planet = new CelestialBody
            {
                Name = "Kerbin",
                BodyType = BodyType.Planet,
                IsHomeWorld = true,
                HasAtmosphere = true,
                CanUseJets = true,
            };

            CelestialBody[] arr = new CelestialBody[] {planet};


            star.AddPlanets(arr);

            var cbPath = $"{settings.CelestialBodyLocation}/{star.Name.ToTitleCase()}.xml";
            //var appPath = $"{settings.SettingsLocation}/Settings.xml";

            var cbFileStream = File.OpenWrite(cbPath);
            //var appFileStream = File.OpenWrite(appPath);

            using (cbFileStream)
            {
                //var APPrepo = new ApplicationSettingsRepository(settings);
                var cbRepository = new CelestialBodyRepository();

                cbRepository.Serialize(cbFileStream, star);
            }
        }
    }
}

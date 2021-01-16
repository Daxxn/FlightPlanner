using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlateModelLibrary
{
   public static class PlateFinder
   {
      #region - Fields & Properties
      public static string AirportPlateRootPath { get; set; }
      private static string[] AllPlatePaths;
      #endregion

      #region - Methods
      public static void OnStartup(string airportPlatePath)
      {
         AirportPlateRootPath = airportPlatePath;
         AllPlatePaths = Directory.GetFiles(airportPlatePath, "*.png", SearchOption.AllDirectories);
      }
      public static List<Plate> FindPlates(AirportData airport)
      {
         List<Plate> results = new List<Plate>();
         foreach (var platePath in AllPlatePaths)
         {
            string plateName = Path.GetFileNameWithoutExtension(platePath);
            string[] plateIdentifiers = plateName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (plateIdentifiers.Length > 0)
            {
               if (plateIdentifiers[0] == airport.IATACode)
               {
                  var newPlate = new Plate
                  {
                     AirportData = airport,
                     MetaData = new PlateMetaData
                     {
                        RegionCode = plateIdentifiers[1],
                        PlateType = plateIdentifiers[2],
                        PlateData = plateIdentifiers[3],
                        Runway = plateIdentifiers[4],
                        ApproachOption = plateIdentifiers[5],
                        Other = plateIdentifiers[6]
                     },
                     PlateUri = new Uri(platePath)
                  };
                  results.Add(newPlate);
               }
            }
         }
         return results;
      }
      #endregion

      #region - Full Properties

      #endregion
   }
}

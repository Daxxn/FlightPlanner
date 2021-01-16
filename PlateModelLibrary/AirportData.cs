using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlateModelLibrary
{
   public class AirportData : Model
   {
      #region - Fields & Properties
      public static List<AirportData> AllAirportData { get; private set; }
      public string IATACode { get; set; }
      public string CityState { get; set; }
      public string CountryCode { get; set; }
      public string Other { get; set; }
      #endregion

      #region - Constructors
      public AirportData() { }
      #endregion

      #region - Methods
      public static void OnStartup()
      {

      }
      #endregion

      #region - Full Properties
      public string FullCode
      {
         get
         {
            return $"{CountryCode}{IATACode}";
         }
      }
      #endregion
   }
}

using System;
using MVVMLibrary;

namespace PlateModelLibrary
{
   public class Plate : Model
   {
      #region - Fields & Properties
      private AirportData _airportData;
      private Uri _plateUri;
      #endregion

      #region - Constructors
      public Plate() { }
      public Plate(string apCode, string countryCode, string url)
      {
         AirportData = new AirportData
         {
            IATACode = apCode,
            CountryCode = countryCode,
         };
         PlateUri = new Uri(url);
      }
      public Plate(AirportData airport, Uri uri)
      {
         AirportData = airport;
         PlateUri = uri;
      }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties
      public AirportData AirportData
      {
         get { return _airportData; }
         set
         {
            _airportData = value;
            OnPropertyChanged();
         }
      }

      public Uri PlateUri
      {
         get { return _plateUri; }
         set
         {
            _plateUri = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}

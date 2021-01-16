using Microsoft.Extensions.Configuration;
using MVVMLibrary;
using PlateModelLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FlightPlannerWPF.ViewModels
{
   public class PlateBrowserViewModel : ViewModel
   {
      #region - Fields & Properties
      private readonly IConfiguration Config = MainViewModel.Config;
      private ObservableCollection<Plate> _plateList;

      #region Commands
      public Command FindPlatesTestCmd { get; set; }
      #endregion

      #endregion

      #region - Constructors
      public PlateBrowserViewModel()
      {
         #region Command Init
         FindPlatesTestCmd = new Command(FindPlatesTest);
         #endregion
         //PlateList = new ObservableCollection<Plate>
         //{
         //   new Plate
         //   {
         //      AirportData = new AirportData
         //      {
         //         IATACode = "ABE",
         //         CityState = "Allentown, Pennsylvania",
         //         CountryCode = "K"
         //      },
         //      PlateUri = new Uri(@""),
         //   }
         //};
      }
      #endregion

      #region - Methods
      public void FindPlatesTest(object p)
      {
         var testAirport = new AirportData
         {
            IATACode = "ABE",
            CountryCode = "K",
            CityState = "Allentown, Pennsylvania"
         };
         PlateList = new ObservableCollection<Plate>(PlateFinder.FindPlates(testAirport));
      }
      #endregion

      #region - Full Properties

      public ObservableCollection<Plate> PlateList
      {
         get { return _plateList; }
         set
         {
            _plateList = value;
            OnPropertyChanged();
         }
      }

      #endregion
   }
}

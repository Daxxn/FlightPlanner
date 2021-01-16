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

      #endregion

      #region - Constructors
      public PlateBrowserViewModel()
      {
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

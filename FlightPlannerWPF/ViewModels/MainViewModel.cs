using Microsoft.Extensions.Configuration;
using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightPlannerWPF.ViewModels
{
   public class MainViewModel : ViewModel
   {
      #region - Fields & Properties
      public static IConfiguration Config { get; private set; }
      #endregion

      #region - Constructors
      public MainViewModel(IConfiguration config)
      {
         Config = config;
      }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties
      
      #endregion
   }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FlightPlannerWPF.ViewModels
{
   public class KneeboardViewModel : ViewModel
   {
      #region - Fields & Properties
      private readonly IConfiguration Config = MainViewModel.Config;

      #region private props
      private string _currentFilePath;
      #endregion

      #region Commands
      public Command OpenTestCmd { get; private set; }
      #endregion
      #endregion

      #region - Constructors
      public KneeboardViewModel()
      {
         OpenTestCmd = new Command(OpenTest);
      }
      #endregion

      #region - Methods
      public void OpenTest(object p)
      {
         OpenFileDialog browser = new OpenFileDialog();

         if (browser.ShowDialog() == true)
         {
            CurrentFilePath = browser.FileName;
         }
      }
      #endregion

      #region - Full Properties

      public string CurrentFilePath
      {
         get { return _currentFilePath; }
         set
         {
            _currentFilePath = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CurrentFileName));
         }
      }

      public string CurrentFileName
      {
         get
         {
            if (CurrentFilePath != null && File.Exists(CurrentFilePath))
            {
               return Path.GetFileName(CurrentFilePath);
            }
            return null;
         }
      }
      #endregion
   }
}

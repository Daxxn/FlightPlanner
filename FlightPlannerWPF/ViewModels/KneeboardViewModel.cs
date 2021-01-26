using FlightPlanParser;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using MVVMLibrary;
using PlateModelLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FlightPlannerWPF.Events;

namespace FlightPlannerWPF.ViewModels
{
   public class KneeboardViewModel : ViewModel
   {
      #region - Fields & Properties
      private readonly IConfiguration Config = MainViewModel.Config;

      public event EventHandler<ImageUpdateEventArgs> UpdateImage;

      #region private props
      private string _currentFilePath;

      private FlightPlan _flightPlan;
      private KneeBoard _kneeboard;

      private Plate _selectedPlate;
      #endregion

      #region Commands
      public Command OpenFlightPlanCmd { get; private set; }
      public Command PrevPlateCmd { get; private set; }
      public Command NextPlateCmd { get; private set; }
      #endregion
      #endregion

      #region - Constructors
      public KneeboardViewModel()
      {
         OpenFlightPlanCmd = new Command(OpenFlightPlan);
         PrevPlateCmd = new Command(PrevPlate);
         NextPlateCmd = new Command(NextPlate);
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

      public void OpenFlightPlan(object p)
      {

         OpenFileDialog browser = new OpenFileDialog
         {
            InitialDirectory = Config.GetValue<string>("DefaultFlightPlanFolder"),
            AddExtension = false,
            Multiselect = false,
            Title = "Open Flight Plan"
         };

         if (browser.ShowDialog() == true)
         {
            CurrentFilePath = browser.FileName;

            FlightPlan = Parser.OpenFlightPlanFile(CurrentFilePath);

            KneeBoard = KneeBoard.Create(FlightPlan);
         }
      }

      private void PrevPlate(object p)
      {
         if (SelectedPlateIndex > 0)
         {
            SelectedPlate = KneeBoard.Plates[SelectedPlateIndex - 1];
         }
      }

      private void NextPlate(object p)
      {
         if (SelectedPlateIndex < KneeBoard.Plates.Count - 1)
         {
            SelectedPlate = KneeBoard.Plates[SelectedPlateIndex + 1];
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

      public FlightPlan FlightPlan
      {
         get { return _flightPlan; }
         set
         {
            _flightPlan = value;
            OnPropertyChanged();
         }
      }

      public KneeBoard KneeBoard
      {
         get { return _kneeboard; }
         set
         {
            _kneeboard = value;
            OnPropertyChanged();
         }
      }

      public Plate SelectedPlate
      {
         get { return _selectedPlate; }
         set
         {
            _selectedPlate = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(SelectedPlateIndex));
            UpdateImage?.Invoke(this, new ImageUpdateEventArgs(value?.PlateFile));
         }
      }
      public int SelectedPlateIndex
      {
         get
         {
            if (SelectedPlate != null && KneeBoard != null)
            {
               return KneeBoard.Plates.IndexOf(SelectedPlate);
            }
            else
            {
               return 0;
            }
         }
      }
      #endregion
   }
}

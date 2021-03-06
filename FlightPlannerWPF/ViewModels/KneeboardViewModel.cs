﻿using FlightPlanParser;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using MVVMLibrary;
using PlateModelLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FlightPlannerWPF.Events;
using System.Collections.ObjectModel;

namespace FlightPlannerWPF.ViewModels
{
   public class KneeboardViewModel : ViewModel
   {
      #region - Fields & Properties
      private readonly IConfiguration Config = MainViewModel.Config;

      public event EventHandler<ImageUpdateEventArgs> UpdateImage;
      public static event EventHandler SelectedWaypointChanged;

      #region private props
      private string _currentFilePath;

      private FlightPlan _flightPlan;
      private KneeBoard _kneeboard;

      private Plate _selectedPlate;
      private Plate _selectedCustomPlate;
      private ATCWaypoint _selectedWaypoint;
      #endregion

      #region Commands
      public Command OpenFlightPlanCmd { get; private set; }
      public Command PrevPlateCmd { get; private set; }
      public Command NextPlateCmd { get; private set; }

      public Command AddCustomCmd { get; private set; }
      public Command RemCustomCmd { get; private set; }
      public Command MoveSelectedUpCmd { get; private set; }
      public Command MoveSelectedDownCmd { get; private set; }
      #endregion
      #endregion

      #region - Constructors
      public KneeboardViewModel()
      {
         OpenFlightPlanCmd = new Command(OpenFlightPlan);
         PrevPlateCmd = new Command(PrevPlate);
         NextPlateCmd = new Command(NextPlate);
         AddCustomCmd = new Command(AddToCustom);
         RemCustomCmd = new Command(DelFromCustom);
         MoveSelectedUpCmd = new Command(MoveSelectedUp);
         MoveSelectedDownCmd = new Command(MoveSelectedDown);
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
            Title = "Open Flight Plan",
            Filter = "PLN|*.PLN|All|*.*"
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
         if (SelectedCustomPlateIndex > 0)
         {
            SelectedCustomPlate = KneeBoard.CustomPlateList[SelectedCustomPlateIndex - 1];
         }
      }

      private void NextPlate(object p)
      {
         if (SelectedCustomPlateIndex < KneeBoard.CustomPlateList.Count - 1)
         {
            SelectedCustomPlate = KneeBoard.CustomPlateList[SelectedCustomPlateIndex + 1];
         }
      }

      private void MoveSelectedUp(object p)
      {
         if (SelectedCustomPlate is null || KneeBoard is null || KneeBoard.CustomPlateList is null) return;

         if (KneeBoard.CustomPlateList.Contains(SelectedCustomPlate))
         {
            var currentIndex = KneeBoard.CustomPlateList.IndexOf(SelectedCustomPlate);
            if (currentIndex > 0)
            {
               KneeBoard.CustomPlateList.Move(currentIndex, currentIndex - 1);
            }
         }
      }

      private void MoveSelectedDown(object p)
      {
         if (SelectedCustomPlate is null || KneeBoard is null || KneeBoard.CustomPlateList is null) return;

         if (KneeBoard.CustomPlateList.Contains(SelectedPlate))
         {
            var currentIndex = KneeBoard.CustomPlateList.IndexOf(SelectedCustomPlate);
            if (currentIndex < KneeBoard.CustomPlateList.Count - 1)
            {
               KneeBoard.CustomPlateList.Move(currentIndex, currentIndex + 1);
            }
         }
      }

      private void AddToCustom(object p)
      {
         if (SelectedPlate is null || KneeBoard is null) return;
         if (KneeBoard.CustomPlateList is null) KneeBoard.CustomPlateList = new ObservableCollection<Plate>();

         if (!KneeBoard.CustomPlateList.Contains(SelectedPlate))
         {
            KneeBoard.CustomPlateList.Add(SelectedPlate);
         }
      }

      private void DelFromCustom(object p)
      {
         if (SelectedCustomPlate is null || KneeBoard is null) return;
         if (KneeBoard.CustomPlateList is null) KneeBoard.CustomPlateList = new ObservableCollection<Plate>();

         if (KneeBoard.CustomPlateList.Contains(SelectedCustomPlate))
         {
            KneeBoard.CustomPlateList.Remove(SelectedCustomPlate);
            if (KneeBoard.CustomPlateList.Count > 0)
            {
               SelectedCustomPlate = KneeBoard.CustomPlateList[0];
            }
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

      public Plate SelectedCustomPlate
      {
         get { return _selectedCustomPlate; }
         set
         {
            _selectedCustomPlate = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(SelectedCustomPlateIndex));
            UpdateImage?.Invoke(this, new ImageUpdateEventArgs(value?.PlateFile));
         }
      }

      public int SelectedCustomPlateIndex
      {
         get
         {
            if (SelectedCustomPlate != null && KneeBoard != null)
            {
               return KneeBoard.CustomPlateList.IndexOf(SelectedCustomPlate);
            }
            else
            {
               return 0;
            }
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

      public ATCWaypoint SelectedWaypoint
      {
         get { return _selectedWaypoint; }
         set
         {
            _selectedWaypoint = value;
            OnPropertyChanged();
            SelectedWaypointChanged?.Invoke(this, null);
         }
      }
      #endregion
   }
}

using Microsoft.Extensions.Configuration;
using MVVMLibrary;
using FlightPlannerWPF.Models;
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

      private Page<Plate> _page;
      private Plate _selectedPlate;
      private int _imageSize;

      #region Commands
      public Command PrevPageCmd { get; private set; }
      public Command NextPageCmd { get; private set; }
      #endregion

      #endregion

      #region - Constructors
      public PlateBrowserViewModel()
      {
         if (Plate.AllPlates != null)
         {
            PlatePage = new Page<Plate>(10, Plate.AllPlates);
         }
         #region Command Init
         PrevPageCmd = new Command(PrevPage);
         NextPageCmd = new Command(NextPage);
         #endregion
      }
      #endregion

      #region - Methods
      private void PrevPage(object p)
      {
         PlatePage.PrevPage();
         SelectedPlate = PlatePage.PageData[0];
      }

      private void NextPage(object p)
      {
         PlatePage.NextPage();
         SelectedPlate = PlatePage.PageData[0];
      }
      #endregion

      #region - Full Properties

      public Page<Plate> PlatePage
      {
         get { return _page; }
         set
         {
            _page = value;
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
         }
      }

      public int ImageSize
      {
         get { return _imageSize; }
         set
         {
            _imageSize = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}

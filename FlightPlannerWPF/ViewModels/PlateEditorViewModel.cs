using FlightPlannerWPF.Events;
using MVVMLibrary;
using FlightPlannerWPF.Models;
using PlateModelLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightPlannerWPF.ViewModels
{
   public class PlateEditorViewModel : ViewModel
   {
      #region - Fields & Properties
      public event EventHandler<ImageUpdateEventArgs> UpdateImage;

      private Page<Plate> _page;
      private Plate _selectedPlate;

      #region Commands
      public Command PrevPageCmd { get; private set; }
      public Command NextPageCmd { get; private set; }
      #endregion
      #endregion

      #region - Constructors
      public PlateEditorViewModel()
      {
         Page = new Page<Plate>(50, Plate.AllPlates);

         PrevPageCmd = new Command((p) => Page.PrevPage());
         NextPageCmd = new Command((p) => Page.NextPage());
      }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties
      public Page<Plate> Page
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
            UpdateImage?.Invoke(this, new ImageUpdateEventArgs(value?.PlateFile));
         }
      }
      #endregion
   }
}

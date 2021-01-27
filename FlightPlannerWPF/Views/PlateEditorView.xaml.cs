using FlightPlannerWPF.Events;
using FlightPlannerWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightPlannerWPF.Views
{
   /// <summary>
   /// Interaction logic for PlateEditorView.xaml
   /// </summary>
   public partial class PlateEditorView : UserControl
   {
      public PlateEditorView()
      {
         var VM = new PlateEditorViewModel();
         DataContext = VM;
         InitializeComponent();

         VM.UpdateImage += UpdateImageEvent;
      }

      private void UpdateImageEvent(object sender, ImageUpdateEventArgs e)
      {
         if (!String.IsNullOrEmpty(e.ImagePath))
         {
            SelectedPlateImage.Source = new BitmapImage(new Uri(e.ImagePath));
         }
         else
         {
            SelectedPlateImage.Source = null;
         }
      }
   }
}

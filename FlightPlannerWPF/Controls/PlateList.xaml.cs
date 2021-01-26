using PlateModelLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FlightPlannerWPF.Controls
{
   /// <summary>
   /// Interaction logic for PlateList.xaml
   /// </summary>
   public partial class PlateList : UserControl
   {
      public string Title
      {
         get { return (string)GetValue(TitleProperty); }
         set { SetValue(TitleProperty, value); }
      }

      public ExpandDirection ExpandDirection
      {
         get { return (ExpandDirection)GetValue(ExpandDirectionProperty); }
         set { SetValue(ExpandDirectionProperty, value); }
      }

      // Using a DependencyProperty as the backing store for ExpandDirection.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ExpandDirectionProperty =
          DependencyProperty.Register("ExpandDirection", typeof(ExpandDirection), typeof(PlateList), new PropertyMetadata(ExpandDirection.Down));


      // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty TitleProperty =
          DependencyProperty.Register("Title", typeof(string), typeof(PlateList), new PropertyMetadata(null));

      public ObservableCollection<Plate> Plates
      {
         get { return (ObservableCollection<Plate>)GetValue(PlatesProperty); }
         set { SetValue(PlatesProperty, value); }
      }

      // Using a DependencyProperty as the backing store for Plates.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty PlatesProperty =
          DependencyProperty.Register("Plates", typeof(ObservableCollection<Plate>), typeof(PlateList), new PropertyMetadata(null));

      public Plate SelectedPlate
      {
         get { return (Plate)GetValue(SelectedPlateProperty); }
         set { SetValue(SelectedPlateProperty, value); }
      }

      // Using a DependencyProperty as the backing store for SelectedPlate.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty SelectedPlateProperty =
          DependencyProperty.Register("SelectedPlate", typeof(Plate), typeof(PlateList), new PropertyMetadata(null));

      public PlateList()
      {
         InitializeComponent();
      }
   }
}

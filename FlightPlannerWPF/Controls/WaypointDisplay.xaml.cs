using FlightPlannerWPF.ViewModels;
using FlightPlanParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
   /// Interaction logic for WaypointDisplay.xaml
   /// </summary>
   public partial class WaypointDisplay : UserControl, INotifyPropertyChanged
   {
      private string _displayType;
      public ATCWaypoint Waypoint
      {
         get { return (ATCWaypoint)GetValue(WaypointProperty); }
         set { SetValue(WaypointProperty, value); }
      }

      // Using a DependencyProperty as the backing store for Waypoint.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty WaypointProperty =
          DependencyProperty.Register("Waypoint", typeof(ATCWaypoint), typeof(WaypointDisplay), new PropertyMetadata(null));

      public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

      public WaypointDisplay()
      {
         InitializeComponent();
         KneeboardViewModel.SelectedWaypointChanged += KneeboardViewModel_SelectedWaypointChanged;
      }

      private void KneeboardViewModel_SelectedWaypointChanged(object sender, EventArgs e)
      {
         if (Waypoint != null)
         {
            if (Waypoint.ATCWaypointType == "Intersection")
            {
               if (Waypoint.DepartureFP is null && Waypoint.ArrivalFP != null)
               {
                  DisplayType = "Arrival";
               }
               else
               {
                  DisplayType = "Departure";
               }
            }
            else
            {
               DisplayType = "Airport";
            }
         }
         else
         {
            DisplayType = "";
         }
      }


      public string DisplayType
      {
         get { return _displayType; }
         set
         {
            _displayType = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayType)));
         }
      }
   }
}

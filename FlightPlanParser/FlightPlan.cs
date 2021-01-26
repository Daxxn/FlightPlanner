using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FlightPlanParser
{
   public class FlightPlan : Model
   {
      #region - Fields & Properties
      public string Title { get; set; }
      public string FPType { get; set; }
      public string RouteType { get; set; }
      public double CruisingAlt { get; set; }
      public string DepartureID { get; set; }
      public string DestinationID { get; set; }
      public ObservableCollection<ATCWaypoint> _waypoints = new ObservableCollection<ATCWaypoint>();
      #endregion

      #region - Constructors
      public FlightPlan() { }
      #endregion

      #region - Methods
      public override string ToString()
      {
         StringBuilder sb = new StringBuilder();
         sb.AppendLine(Title);
         sb.AppendLine(FPType);
         sb.AppendLine(RouteType);
         sb.AppendLine($"Cruise Alt: {CruisingAlt}");
         sb.AppendLine($"Departure ID: {DepartureID}");
         sb.AppendLine($"Destination ID: {DestinationID}\n");
         foreach (var wp in Waypoints)
         {
            sb.AppendLine(wp.ToString());
         }
         return sb.ToString();
      }

      private string GetIATACode(string id)
      {
         if (String.IsNullOrEmpty(id)) return null;

         if (id.Length == 4 && id[0] == 'K')
         {
            return id.TrimStart('K');
         }

         return id;
      }
      #endregion

      #region - Full Properties
      public ObservableCollection<ATCWaypoint> Waypoints
      {
         get { return _waypoints; }
         set
         {
            _waypoints = value;
            OnPropertyChanged();
         }
      }
      public string DepartureIATA
      {
         get
         {
            return GetIATACode(DepartureID);
         }
      }

      public string DestinationIATA
      {
         get
         {
            return GetIATACode(DestinationID);
         }
      }
      #endregion
   }
}

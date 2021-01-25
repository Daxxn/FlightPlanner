using System;
using System.Collections.Generic;
using System.Text;

namespace FlightPlanParser
{
   public class FlightPlan
   {
      #region - Fields & Properties
      public string Title { get; set; }
      public string FPType { get; set; }
      public string RouteType { get; set; }
      public double CruisingAlt { get; set; }
      public string DepartureID { get; set; }
      public string DestinationID { get; set; }
      public List<ATCWaypoint> Waypoints { get; set; } = new List<ATCWaypoint>();
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
      #endregion

      #region - Full Properties

      #endregion
   }
}

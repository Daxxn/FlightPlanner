using System;
using System.Collections.Generic;
using System.Text;

namespace FlightPlanParser
{
   public class ATCWaypoint
   {
      #region - Fields & Properties
      public string id { get; set; }
      public string ATCWaypointType { get; set; }
      public double SpeedMaxFP { get; set; }
      public string DepartureFP { get; set; }
      public string ArrivalFP { get; set; }
      public string ApproachTypeFP { get; set; }
      public int RunwayNumberFP { get; set; }
      public int Alt1FP { get; set; }
      public string AltDescFP { get; set; }
      public string RunwayDesignatorFP { get; set; }
      public ICAO ICAO { get; set; }
      #endregion

      #region - Constructors
      public ATCWaypoint() { }
      #endregion

      #region - Methods
      public override string ToString()
      {
         StringBuilder sb = new StringBuilder();
         sb.AppendLine($"\tType: {ATCWaypointType}");
         sb.Append(SpeedMaxFP != -1 ? $"\tSpeed: {SpeedMaxFP}\n" : "");
         sb.Append(DepartureFP != null ? $"\tDep: {DepartureFP}" : "");
         sb.Append(ArrivalFP != null ? $"\tArr: {ArrivalFP}" : "");
         sb.Append(ApproachTypeFP != null ? $"\tApp Type: {ApproachTypeFP}" : "");
         sb.Append(RunwayNumberFP != 0 ? $"\tRunway: {RunwayNumberFP}" : "");
         sb.Append(Alt1FP != 0 ? $"\tAlt: {Alt1FP}" : "");
         sb.Append(RunwayDesignatorFP != null ? $"\tRunway Side: {RunwayDesignatorFP}" : "");
         sb.AppendLine(ICAO is null ? "No ICAO" : ICAO.ToString());
         return sb.ToString();
      }
      #endregion

      #region - Full Properties

      #endregion
   }
}

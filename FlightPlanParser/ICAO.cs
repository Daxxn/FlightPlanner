using System;
using System.Collections.Generic;
using System.Text;

namespace FlightPlanParser
{
   public class ICAO
   {
      #region - Fields & Properties
      public string ICAOIdent { get; set; }
      public string ICAORegion { get; set; }
      #endregion

      #region - Constructors
      public ICAO() { }
      #endregion

      #region - Methods
      public override string ToString()
      {
         return $"\t\t{ICAOIdent} : {ICAORegion}";
      }
      #endregion

      #region - Full Properties

      #endregion
   }
}

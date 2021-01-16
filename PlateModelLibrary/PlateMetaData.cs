using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlateModelLibrary
{
   public class PlateMetaData : Model
   {
      #region - Fields & Properties
      public string RegionCode { get; set; }
      public string Runway { get; set; }
      public string PlateType { get; set; }
      public string PlateData { get; set; }
      public string ApproachOption { get; set; }
      public string Other { get; set; }
      #endregion

      #region - Constructors
      public PlateMetaData() { }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties

      #endregion
   }
}

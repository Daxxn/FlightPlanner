using System;
using System.Collections.Generic;
using System.Text;
using JsonReaderLibrary;

namespace FrequencyNotesLibrary
{
   public class FrequencyNotesModel
   {
      #region - Fields & Properties
      public string FlightPlanFilePath { get; private set; }
      public List<Frequency> Frequencies { get; private set; }
      #endregion

      #region - Constructors
      public FrequencyNotesModel() { }
      public FrequencyNotesModel(string flightPlanPath, List<Frequency> frequencies)
      {
         FlightPlanFilePath = flightPlanPath;
         Frequencies = frequencies;
      }
      #endregion

      #region - Methods
      public static void SaveFrequencies(string path, FrequencyNotesModel data)
      {
         JsonReader.SaveJsonFile(path, data, true, true);
      }
      #endregion

      #region - Full Properties

      #endregion
   }
}

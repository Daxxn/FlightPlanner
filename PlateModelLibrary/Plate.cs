using System;
using System.Collections.Generic;
using JsonReaderLibrary;
using MVVMLibrary;

namespace PlateModelLibrary
{
   public class Plate : Model
   {
      #region - Fields & Properties
      public static List<Plate> AllPlates { get; private set; }
      private string _IATACode;
      private PlateType _type;
      private string _regionCode;
      private string _approachType;
      private string _name;
      private string _runway;
      private string _approachOption;
      private string _other;
      private string _plateFile;
      #endregion

      #region - Constructors
      public Plate() { }
      #endregion

      #region - Methods
      public static void OnStartup(string platesFilePath)
      {
         try
         {
            AllPlates = JsonReader.OpenJsonFile<List<Plate>>(platesFilePath);
         }
         catch (Exception)
         {
            throw;
         }
      }
      #endregion

      #region - Full Properties

      public string IATACode
      {
         get { return _IATACode; }
         set
         {
            _IATACode = value;
            OnPropertyChanged();
         }
      }

      public PlateType Type
      {
         get { return _type; }
         set
         {
            _type = value;
            OnPropertyChanged();
         }
      }

      public string RegionCode
      {
         get { return _regionCode; }
         set
         {
            _regionCode = value;
            OnPropertyChanged();
         }
      }

      public string ApproachType
      {
         get { return _approachType; }
         set
         {
            _approachType = value;
            OnPropertyChanged();
         }
      }

      public string Name
      {
         get { return _name; }
         set
         {
            _name = value;
            OnPropertyChanged();
         }
      }

      public string Runway
      {
         get { return _runway; }
         set
         {
            _runway = value;
            OnPropertyChanged();
         }
      }

      public string ApproachOption
      {
         get { return _approachOption; }
         set
         {
            _approachOption = value;
            OnPropertyChanged();
         }
      }

      public string Other
      {
         get { return _other; }
         set
         {
            _other = value;
            OnPropertyChanged();
         }
      }

      public string PlateFile
      {
         get { return _plateFile; }
         set
         {
            _plateFile = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}

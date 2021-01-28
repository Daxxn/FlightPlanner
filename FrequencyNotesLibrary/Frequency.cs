using MVVMLibrary;
using System;

namespace FrequencyNotesLibrary
{
   public class Frequency : Model
   {
      #region - Fields & Properties
      public string _name;
      public int _freqTop = 120;
      public int _freqBot = 0;
      #endregion

      #region - Constructors
      public Frequency() => Name = "Fr";
      public Frequency(string name)
      {
         Name = name;
      }
      #endregion

      #region - Methods
      private int CheckFreqInput(int val, bool isTop = true)
      {
         if (val > 999)
         {
            return 999;
         }
         else if (val <= 0)
         {
            return isTop ? 1 : 0;
         }
         else
         {
            return val;
         }
      }
      #endregion

      #region - Full Properties
      public string Name
      {
         get => _name;
         set
         {
            _name = value;
            OnPropertyChanged();
         }
      }

      public int FreqTop
      {
         get => _freqTop;
         set
         {
            _freqTop = CheckFreqInput(value);
            OnPropertyChanged();
            OnPropertyChanged(nameof(Freq));
         }
      }
      public int FreqBottom
      {
         get => _freqBot;
         set
         {
            _freqBot = CheckFreqInput(value, false);
            OnPropertyChanged();
            OnPropertyChanged(nameof(Freq));
         }
      }

      public double Freq
      {
         get => FreqTop + FreqBottom * 0.001;
      }
      #endregion
   }
}

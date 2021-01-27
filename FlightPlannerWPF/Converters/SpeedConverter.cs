using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace FlightPlannerWPF.Converters
{
   public class SpeedConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value is int numI)
         {
            return numI == -1 ? "N/A" : $"{numI} knts";
         }
         else if (value is double numD)
         {
            return numD == -1 ? "N/A" : $"{numD} knts";
         }
         else
         {
            return value;
         }
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return null;
      }
   }
}

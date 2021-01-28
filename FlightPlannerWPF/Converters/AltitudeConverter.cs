using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace FlightPlannerWPF.Converters
{
   public class AltitudeConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value is int num)
         {
            if (num < 10000)
            {
               return num != 0 ? $"{num} ASL" : "N/A";
            }
            else
            {
               return $"FL{num * 0.001}";
            }
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

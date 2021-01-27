using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace FlightPlannerWPF.Converters
{
   public class VisibilityNullConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value is null)
         {
            return Visibility.Hidden;
         }
         return Visibility.Visible;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return null;
      }
   }
}

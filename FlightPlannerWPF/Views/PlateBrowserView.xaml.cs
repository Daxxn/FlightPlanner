using FlightPlannerWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightPlannerWPF.Views
{
   /// <summary>
   /// Interaction logic for PlateBrowserView.xaml
   /// </summary>
   public partial class PlateBrowserView : UserControl
   {
      public PlateBrowserView()
      {
         DataContext = new PlateBrowserViewModel();
         InitializeComponent();
      }
   }
}

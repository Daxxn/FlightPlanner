﻿using FlightPlannerWPF.Events;
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
   /// Interaction logic for KneeboardView.xaml
   /// </summary>
   public partial class KneeboardView : UserControl
   {
      public KneeboardView()
      {
         var VM = new KneeboardViewModel();
         DataContext = VM;
         VM.UpdateImage += UpdateImageEvent;
         InitializeComponent();
      }

      private void UpdateImageEvent(object sender, ImageUpdateEventArgs e)
      {
         if (!String.IsNullOrEmpty(e.ImagePath))
         {
            SelectedPlateImage.Source = new BitmapImage(new Uri(e.ImagePath));
         }
         else
         {
            SelectedPlateImage.Source = null;
         }
      }
   }
}

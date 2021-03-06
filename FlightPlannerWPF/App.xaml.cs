﻿using FlightPlannerWPF.ViewModels;
using Microsoft.Extensions.Configuration;
using PlateModelLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightPlannerWPF
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      private IConfiguration BuildConfig()
      {
         IConfigurationBuilder config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
         return config.Build();
      }
      protected override void OnStartup(StartupEventArgs e)
      {
         var config = BuildConfig();
         AirportData.OnStartup(config.GetValue<string>("AirportDataFilePath"));
         Plate.OnStartup(config.GetValue<string>("PlatesFilePath"));
         var mainView = new MainWindow(new MainViewModel(config));
         mainView.Show();
      }
   }
}

using FlightPlanParser;
using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PlateModelLibrary
{
   public class KneeBoard : Model
   {
      #region - Fields & Properties
      private Plate _departure;
      private Plate _destination;
      private ObservableCollection<Plate> _plates;
      private ObservableCollection<Plate> _flightPlanPlates;
      #endregion

      #region - Constructors
      public KneeBoard() { }
      #endregion

      #region - Methods
      public static KneeBoard Create(FlightPlan flightPlan)
      {
         var newKneeboard = new KneeBoard();
         var allPlates = new List<Plate>();
         if (!String.IsNullOrEmpty(flightPlan.DepartureIATA))
         {
            newKneeboard.DepartureAPDiagram = FindAirportDiagramPlate(flightPlan.DepartureIATA);
            allPlates = FindPlatesByAirport(flightPlan.DepartureIATA);
         }
         if (!String.IsNullOrEmpty(flightPlan.DestinationIATA))
         {
            newKneeboard.DestinationAPDiagram = FindAirportDiagramPlate(flightPlan.DestinationIATA);
            allPlates.AddRange(FindPlatesByAirport(flightPlan.DestinationIATA));
         }

         if (allPlates.Count > 0)
         {
            newKneeboard.Plates = new ObservableCollection<Plate>(allPlates);
         }

         //List<string> foundICAOCodes = new List<string>();
         //foreach (var wp in flightPlan.Waypoints)
         //{
         //   if (wp.ICAO != null && !foundICAOCodes.Contains(wp.ICAO.ICAOIdent))
         //   {
         //      foundICAOCodes.Add(wp.ICAO.ICAOIdent);
         //   }
         //}

         //foreach (var code in foundICAOCodes)
         //{
         //   newKneeboard.Plates.Add(FindPlate(code));
         //}

         return newKneeboard;
      }

      private static Plate FindPlate(string value, string propName = "IATACode")
      {
         var prop = new Plate().GetType().GetProperty(propName);
         if (prop is null || prop.PropertyType != typeof(string))
         {
            return Plate.AllPlates.Find(plate => plate.IATACode == value);
         }
         else
         {
            return Plate.AllPlates.Find(plate => prop.GetValue(plate) as string == value);
         }
      }

      private static List<Plate> FindPlates(string value, string propName = "IATACode")
      {
         var prop = new Plate().GetType().GetProperty(propName);
         if (prop is null || prop.PropertyType != typeof(string))
         {
            return Plate.AllPlates.FindAll(plate => plate.IATACode == value);
         }
         else
         {
            return Plate.AllPlates.FindAll(plate => prop.GetValue(plate) as string == value);
         }
      }

      private static List<Plate> FindPlatesByAirport(string value)
      {
         if (!String.IsNullOrEmpty(value))
         {
            return Plate.AllPlates.FindAll(plate => plate.IATACode == value);
         }
         else
         {
            return new List<Plate>();
         }
      }

      private static Plate FindAirportDiagramPlate(string IATACode)
      {
         return Plate.AllPlates.Find(plate => plate.IATACode == IATACode && plate.Type == PlateType.AirportDiagram);
      }
      #endregion
      #region - Full Properties
      public Plate DepartureAPDiagram
      {
         get { return _departure; }
         set
         {
            _departure = value;
            OnPropertyChanged();
         }
      }

      public Plate DestinationAPDiagram
      {
         get { return _destination; }
         set
         {
            _destination = value;
            OnPropertyChanged();
         }
      }

      public ObservableCollection<Plate> Plates
      {
         get { return _plates; }
         set
         {
            _plates = value;
            OnPropertyChanged();
         }
      }

      public ObservableCollection<Plate> FlightPlanPlates
      {
         get { return _flightPlanPlates; }
         set
         {
            _flightPlanPlates = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}

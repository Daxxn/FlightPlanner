using FlightPlanParser;
using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PlateModelLibrary
{
   public class KneeBoard : Model
   {
      #region - Fields & Properties
      private Plate _departureAirport;
      private Plate _destinationAirport;
      private List<Plate> _arrival;
      private List<Plate> _departure;
      private ObservableCollection<Plate> _plates;
      private ObservableCollection<Plate> _customPlateList;
      private string _departureName;
      private string _arrivalName;
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

         if (flightPlan.FPType == "IFR")
         {
            foreach (var wp in flightPlan.Waypoints)
            {
               if (wp.ATCWaypointType == "Intersection")
               {
                  if (wp.DepartureFP != null && wp.ArrivalFP == null)
                  {
                     if (newKneeboard.DepartureSID is null && !String.IsNullOrEmpty(wp.DepartureFP))
                     {
                        newKneeboard.DepartureSID = wp.DepartureFP;
                     }
                     newKneeboard.DeparturePlates = newKneeboard.Plates.Where(
                        plate => CompareNames(plate.Name, wp.DepartureFP)
                        && plate.Type == PlateType.ArrivalDeparture
                     ).ToList();
                  }
                  else if (wp.DepartureFP == null && wp.ArrivalFP != null)
                  {
                     if (newKneeboard.ArrivalSTAR is null && !String.IsNullOrEmpty(wp.ArrivalFP))
                     {
                        newKneeboard.ArrivalSTAR = wp.ArrivalFP;
                     }
                     newKneeboard.ArrivalPlates = newKneeboard.Plates.Where(
                        plate => plate.Name == wp.ArrivalFP
                     ).ToList();
                  }
               }
            }
         }

         return newKneeboard;
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

      public static Plate FindPlateByType(string value, PlateType type)
      {
         if (String.IsNullOrEmpty(value)) return null;

         switch (type)
         {
            case PlateType.Approach:
               return Plate.AllPlates.Find(plate => plate.IATACode == value && plate.Type == type);
            case PlateType.ArrivalDeparture:
               return Plate.AllPlates.Find(plate => plate.Name == value && plate.Type == type);
            case PlateType.AirportDiagram:
               return FindAirportDiagramPlate(value);
            default:
               throw new ArgumentException($"Plate type not found. {type}");
         }
      }

      private static List<Plate> FindPlatesByDepArr(string IATACode)
      {
         if (String.IsNullOrEmpty(IATACode)) return null;

         return Plate.AllPlates.FindAll(plate => plate.IATACode == IATACode);
      }

      private static bool CompareNames(string plateName, string waypointName)
      {
         if (plateName != "NA-")
         {
            int plNameValue = 0;
            List<char> checkedChars = new List<char>();
            double charMatch = 0;

            foreach (var wpChar in waypointName)
            {
               if (plateName.Contains(wpChar) && !checkedChars.Contains(wpChar))
               {
                  plNameValue++;
               }
               if (!checkedChars.Contains(wpChar))
               {
                  checkedChars.Add(wpChar);
               }
            }

            if (plNameValue > 0)
            {
               charMatch = (double)plNameValue / (double)waypointName.Length;
            }

            return charMatch > 0.6;
         }
         return false;
      }
      #endregion

      #region - Full Properties
      public Plate DepartureAPDiagram
      {
         get { return _departureAirport; }
         set
         {
            _departureAirport = value;
            OnPropertyChanged();
         }
      }

      public Plate DestinationAPDiagram
      {
         get { return _destinationAirport; }
         set
         {
            _destinationAirport = value;
            OnPropertyChanged();
         }
      }

      public List<Plate> DeparturePlates
      {
         get { return _departure; }
         set
         {
            _departure = value;
            OnPropertyChanged();
         }
      }

      public List<Plate> ArrivalPlates
      {
         get { return _arrival; }
         set
         {
            _arrival = value;
            OnPropertyChanged();
         }
      }


      public string DepartureSID
      {
         get { return _departureName; }
         set
         {
            _departureName = value;
            OnPropertyChanged();
         }
      }

      public string ArrivalSTAR
      {
         get { return _arrivalName; }
         set
         {
            _arrivalName = value;
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

      public ObservableCollection<Plate> CustomPlateList
      {
         get { return _customPlateList; }
         set
         {
            _customPlateList = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}

using System;
using System.Xml.XPath;

namespace FlightPlanParser
{
   public class Parser
   {
      public static FlightPlan OpenFlightPlanFile(string path)
      {
         var newFP = new FlightPlan();
         XPathDocument doc = new XPathDocument(path);
         var nav = doc.CreateNavigator();
         var fpNode = nav.SelectSingleNode("/SimBase.Document/FlightPlan.FlightPlan");
         var nodes = fpNode.SelectChildren(XPathNodeType.Element);
         foreach (var n in nodes)
         {
            if (n is XPathNavigator x)
            {
               switch (x.Name)
               {
                  case "Title":
                     newFP.Title = x.Value;
                     break;
                  case "FPType":
                     newFP.FPType = x.Value;
                     break;
                  case "RouteType":
                     newFP.RouteType = x.Value;
                     break;
                  case "DepartureID":
                     newFP.DepartureID = x.Value;
                     break;
                  case "DestinationID":
                     newFP.DestinationID = x.Value;
                     break;
                  case "ATCWaypoint":
                     newFP.Waypoints.Add(
                        ParseNode(
                           x.SelectChildren(XPathNodeType.Element), typeof(ATCWaypoint)
                           ) as ATCWaypoint
                        );
                     break;
                  default:
                     break;
               }
            }
         }
         return newFP;
      }

      public static object ParseNode(XPathNodeIterator node, Type type)
      {
         var output = type.GetConstructor(new Type[0]).Invoke(new object[0]);

         foreach (var n in node)
         {
            if (n is XPathNavigator x)
            {
               var prop = type.GetProperty(x.Name);
               if (prop != null)
               {
                  if (prop.PropertyType == typeof(string))
                  {
                     prop.SetValue(output, x.Value);
                  }
                  else if (prop.PropertyType.BaseType == typeof(object))
                  {
                     prop.SetValue(output, ParseNode(x.SelectChildren(XPathNodeType.Element), prop.PropertyType));
                  }
                  else if (prop.PropertyType == typeof(double))
                  {
                     var success = Double.TryParse(x.Value, out double result);
                     prop.SetValue(output, result);
                  }
                  else if (prop.PropertyType == typeof(int))
                  {
                     var success = Int32.TryParse(x.Value, out int result);
                     prop.SetValue(output, result);
                  }
               }
            }
         }

         return output;
      }
   }
}

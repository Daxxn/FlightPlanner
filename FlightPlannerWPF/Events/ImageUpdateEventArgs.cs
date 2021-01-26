using System;
using System.Collections.Generic;
using System.Text;

namespace FlightPlannerWPF.Events
{
   public class ImageUpdateEventArgs : EventArgs
   {
      public string ImagePath { get; private set; }

      public ImageUpdateEventArgs() { }
      public ImageUpdateEventArgs(string imagePath)
      {
         ImagePath = imagePath;
      }
   }
}

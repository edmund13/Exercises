using System.Collections.Generic;


namespace Trains
{
   public class Route
    {

       public Route()
       {
           Tracks = new List<Track>();

       }

        public IList<Track> Tracks { get; set; }

        public int distance { get; set; }

    }
}

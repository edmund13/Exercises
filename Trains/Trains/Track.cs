using System.Collections.Generic;


namespace Trains
{
    public class Track
    {

        public Track()
        {
            Children = new List<Track>();
        }

        public char startNode { get; set; }

        public char endNode { get; set; }

        public int distance { get; set; }

        public IList<Track> Children { get; set; }
        
    }
}

using System.Collections.Generic;


namespace Trains
{
    public class TraversedRoutes
    {

        public TraversedRoutes()
        {
            Routes = new List<Route>();
        }

        public IList<Route> Routes { get; set; }
    }
}

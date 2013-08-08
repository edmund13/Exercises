using System.Collections.Generic;
using System.Linq;


namespace Trains
{
    public class City
    {
        public IList<Track> Routes { get; set; }

        public City()
        {
            Routes = new List<Track>();    
        }

        public object GetRoutesCount()
        {
            return Routes.Count;
        }

        public Route GetShortestPath(char startNode, char endNode)
        {
            TraversedRoutes traversedRoutes = TraverseRoute(startNode, endNode);

            int shortestDistance = traversedRoutes.Routes.Min(x => x.distance);

            return traversedRoutes.Routes.First(x => x.distance == shortestDistance);
        }

        public TraversedRoutes TraverseRoute(char startNode, char endNode, int maximumDistance = -1, int numberOfStops = -1)
        {
            SetupChildren();

            TraversedRoutes journeysTraversed = new TraversedRoutes();

            TraversedRoutes allFoundJourneys = GetAllTraversableRoutes(startNode, maximumDistance, numberOfStops);

            IList<Route> validJourneys = allFoundJourneys.Routes.Where(x => x.Tracks.Last().endNode == endNode).ToList();

            journeysTraversed.Routes = validJourneys;

            return journeysTraversed;
        }

        private TraversedRoutes GetAllTraversableRoutes(char startNode, int maxDistance, int maxNumberOfStops)
        {

            bool useDistance = maxDistance != -1;
            bool useStops = maxNumberOfStops != -1;

            TraversedRoutes traversableRoutes = new TraversedRoutes();

            IEnumerable<Track> initialTracks = GetInitialTracks(startNode);

            if (!GetInitialTracks(startNode).Any())
            {
                return traversableRoutes;
            }

            foreach (Track track in initialTracks)
            {
                Route route = new Route();

                route.Tracks.Add(track);

                route.distance = GetRouteDistance(route);

                traversableRoutes.Routes.Add(route);
            }

            for (int i = 0; i < traversableRoutes.Routes.Count; i++)
            {
                Route[] allRoutes = traversableRoutes.Routes.ToArray();

                Route curentRoute = allRoutes[i];
                
                Track lastTrackInRoute = curentRoute.Tracks.Last();

                if (lastTrackInRoute.Children.Any())
                {
                    foreach (Track nextTrack in lastTrackInRoute.Children)
                    {

                        bool allowLooping = ( ( ( curentRoute.distance + nextTrack.distance ) < maxDistance ) && useDistance )
                                || (curentRoute.Tracks.Count < maxNumberOfStops && useStops);
;
                        if ((!AlreadyExistsInJourney(curentRoute, nextTrack) && !useDistance) || allowLooping)
                        {
                            traversableRoutes.Routes.Add(GetRoute(curentRoute, nextTrack));
                        }
                    }
                }
            }

            return traversableRoutes;
        }

        private Route GetRoute(Route curentRoute, Track nextTrack)
        {
            Route newRoute = new Route();

            foreach (Track previousRoute in curentRoute.Tracks)
            {
                newRoute.Tracks.Add(previousRoute);
            }

            newRoute.Tracks.Add(nextTrack);

            newRoute.distance = GetRouteDistance(newRoute);

            return newRoute;
        }

        private bool AlreadyExistsInJourney(Route route, Track track)
        {
            return route.Tracks.Any(x => x.startNode == track.startNode && x.endNode == track.endNode);
        }

        private int GetRouteDistance(Route route)
        {
            if (!route.Tracks.Any())
            {
                return 0;
            }

            return route.Tracks.Sum(x => x.distance);
        }


        private void SetupChildren()
        {
            foreach (Track route in Routes)
            {
                route.Children = GetChildren(route);
            }
        }

        private IList<Track> GetChildren(Track track)
        {
            return Routes.Where(x => x.startNode == track.endNode).ToList();
        }

        private IEnumerable<Track> GetInitialTracks(char startNode)
        {
            return Routes.Where(x => x.startNode == startNode).ToList();
        }  

        public void AddTrack(Track track)
        {
            Routes.Add(track);
        }


        public IList<Track> CreateTracks(string requiredRoute)
        {
            IList<Track> tracks = new List<Track>();

            char[] nodes = requiredRoute.ToCharArray();

            for(int i = 0; i < nodes.Length - 1; i++) 
            {
                Track track = new Track
                {
                    startNode = nodes[i],
                    endNode = nodes[i + 1]
                };

                tracks.Add(track);

            }
            return tracks;
        }

        public int GetDistanceForRoute(Route route)
        {
            int distance = 0;

            foreach (Track track in route.Tracks)
            {
                bool trackExists = Routes.Any(x => x.startNode == track.startNode && track.endNode == x.endNode);

                if(!trackExists)
                {
                    return 0;
                }

                distance += Routes.Single(x => x.startNode == track.startNode && track.endNode == x.endNode).distance;
            }

            return distance;
        }

        public TraversedRoutes GetNumberOfTripsWithExactNumberOfStops(char startNode, char endNode, int numberOfStops)
        {
            TraversedRoutes allRoutes = TraverseRoute(startNode, endNode, -1, numberOfStops);

            return new TraversedRoutes
            {
                    Routes = allRoutes.Routes.Where(x => x.Tracks.Count == numberOfStops).ToList()
            };
        }

        public TraversedRoutes GetNumberOfTripsWithMaxNumberOfStops(char startNode, char endNode, int maxNumberOfStops)
        {
            TraversedRoutes allRoutes = TraverseRoute(startNode, endNode, -1, maxNumberOfStops);

            return new TraversedRoutes
            {
                Routes = allRoutes.Routes.Where(x => x.Tracks.Count <= maxNumberOfStops).ToList()
            };
        }
    }
}

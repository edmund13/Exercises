using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trains;


namespace TrainsTests
{
    [TestClass]
    public class TrainsTests
    {
        
        /* STRUCTURE
         * --------
         * Each Alphabetic Letter (capsLock) Represents a node/City
         * Two nodes joined together e.g AB represent a Track
         * Two or more tracks joined together are called a Route e.g ABCD
         * Two or More Different or alternate routes are called TraversedRoutes e.g ABC and ABE
         * 
         */

        private City _city;

        [TestInitialize]
        public void SetUpTown()
        {
            _city = new City();

            foreach (Track route in GetTracks())
            {
                _city.AddTrack(route);
            }
        }

        [TestMethod]
        public void CreateRoutes()
        {

            //Arrange
            _city = new City();

            //Act
            foreach (Track track in GetTracks())
            {
                _city.AddTrack(track);
            }

            //Assert
            Assert.AreEqual(_city.GetRoutesCount(), 9);
        }

        [TestMethod]
        public void GetDistanceForRoute()
        {
            //Arrange
            const string pathForRoute1 = "ABC";
            const string pathForRoute2 = "AD";
            const string pathForRoute3 = "ADC";
            const string pathForRoute4 = "AEBCD";
            const string pathForRoute5 = "AED";

            Route route1 = GetRoute(pathForRoute1);
            Route route2 = GetRoute(pathForRoute2);
            Route route3 = GetRoute(pathForRoute3);
            Route route4 = GetRoute(pathForRoute4);
            Route route5 = GetRoute(pathForRoute5);
                                   
            //Act
            int route1Distance = _city.GetDistanceForRoute(route1);
            int route2Distance = _city.GetDistanceForRoute(route2);
            int route3Distance = _city.GetDistanceForRoute(route3);
            int route4Distance = _city.GetDistanceForRoute(route4);
            int route5Distance = _city.GetDistanceForRoute(route5);

            //Assert
            Assert.AreEqual(route1Distance, 9);
            Assert.AreEqual(route2Distance, 5);
            Assert.AreEqual(route3Distance, 13);
            Assert.AreEqual(route4Distance, 22);
            Assert.AreEqual(route5Distance, 0); //0: No such Root
        }

        private Route GetRoute(string pathForRoute1)
        {
            return new Route
            {
                Tracks = _city.CreateTracks(pathForRoute1),
                distance = 0
            };
        }

        [TestMethod]
        public void GetNumberOfTripsWithMaxNumberOfStops()
        {
            //Arrange
            const char startNode = 'C';
            const char endNode = 'C';
            const int maximumNumberOfStops = 3;

            //Act
            TraversedRoutes routes = _city.GetNumberOfTripsWithMaxNumberOfStops(startNode, endNode, maximumNumberOfStops);

            int numberOfRoutes = routes.Routes.Count;

            //Assert
            Assert.AreEqual(2, numberOfRoutes);

        }
        
        [TestMethod]
        public void GetNumberOfTripsWithExactNumberOfStops()
        {

            //Arrange
            const char startNode = 'A';
            const char endNode = 'C';
            const int numberOfStops = 4;

            //Act
            TraversedRoutes routes = _city.GetNumberOfTripsWithExactNumberOfStops(startNode, endNode, numberOfStops);

            int numberOfRoutes = routes.Routes.Count;

            //Assert
            Assert.AreEqual(3, numberOfRoutes);
            
        }

        [TestMethod]
        public void GetShortestPath()
        {
            //Arrange
            const char startNode = 'A';
            const char endNode = 'C';

            const char startNode2 = 'B';
            const char endNode2 = 'B';

            //Act
            Route shortestPath = _city.GetShortestPath(startNode, endNode);
            Route shortestPath2 = _city.GetShortestPath(startNode2, endNode2);

            //Assert
            Assert.AreEqual(shortestPath.distance, 9);
            Assert.AreEqual(shortestPath2.distance, 9);
       
        }

        [TestMethod]
        public void GetRoutesUsingMaximumDistance()
        {
            //Arrange
            const char startNode = 'C';
            const char endNode = 'C';
            const int maximumDistanceToCover = 30;

            //Act
            TraversedRoutes traversedRoutes = _city.TraverseRoute(startNode, endNode, maximumDistanceToCover);

            IList<Route> foundRoutes = traversedRoutes.Routes;

            //Assert
            Assert.AreEqual(7, foundRoutes.Count);
        }

        [TestMethod]
        public void GetRouteswithoutRepeatingTracks()
        {
            //Arrange
            const char startNode = 'C';
            const char endNode = 'C';
           
            //Act
            TraversedRoutes traversedRoutes = _city.TraverseRoute(startNode, endNode);

            IList<Route> foundRoutes = traversedRoutes.Routes;
            
            //Assert
            Assert.AreEqual(5, foundRoutes.Count);
        }

        

       

      

        

        

        [TestMethod]
        public void CreateTracks()
        {
            const string requiredRoute = "CEBCEBCEBC";

            Route route = new Route();

            route.Tracks = _city.CreateTracks(requiredRoute);

            Assert.AreEqual(route.Tracks.Count, 9);
        }

        private IEnumerable<Track> GetTracks()
        {

            IList<Track> routes = new List<Track>();
            Track track = new Track
            {
                    startNode = 'A',
                    endNode = 'B',
                    distance = 5
            };

            Track route2 = new Track
            {
                    startNode = 'B',
                    endNode = 'C',
                    distance = 4
            };

            Track route3 = new Track
            {
                    startNode = 'C',
                    endNode = 'D',
                    distance = 8
            };

            Track route4 = new Track
            {
                    startNode = 'D',
                    endNode = 'C',
                    distance = 8
            };

            Track route5 = new Track
            {
                    startNode = 'D',
                    endNode = 'E',
                    distance = 6
            };

            Track route6 = new Track
            {
                    startNode = 'A',
                    endNode = 'D',
                    distance = 5
            };

            Track route7 = new Track
            {
                    startNode = 'C',
                    endNode = 'E',
                    distance = 2
            };

            Track route8 = new Track
            {
                    startNode = 'E',
                    endNode = 'B',
                    distance = 3
            };

            Track route9 = new Track
            {
                    startNode = 'A',
                    endNode = 'E',
                    distance = 7
            };

            routes.Add(track);
            routes.Add(route2);
            routes.Add(route3);
            routes.Add(route4);
            routes.Add(route5);
            routes.Add(route6);
            routes.Add(route7);
            routes.Add(route8);
            routes.Add(route9);

            return routes;
        }
        
    }
}

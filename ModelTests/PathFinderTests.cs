using System.Collections.Generic;
using System.Drawing;
using Model;
using NUnit.Framework;

namespace ModelTests
{
    public class PathFinderTests
    {
        [TestCase(5, 5, 5, 4)]
        [TestCase(5, 5, 4, 5)]
        [TestCase(5, 5, 5, 6)]
        [TestCase(5, 5, 6, 5)]
        public void OnePointPath(int x1, int y1, int x2, int y2)
        {
            var start = new Point(x1, y1);
            var end = new Point(x2, y2);
            var map = InitializeMap(10, 10);
            var path = PathFinder.FindPaths(map, start, end);
            Assert.AreEqual(end, path.Value);
            Assert.AreEqual(2, path.Length);
        }
        
        [TestCase(0, 0, 1, 0)]
        [TestCase(0, 0, 0, 1)]
        [TestCase(0, 0, -1, 0)]
        [TestCase(0, 0, 0, -1)]
        public void IsNull(int x1, int y1, int x2, int y2)
        {
            var start = new Point(x1, y1);
            var end = new Point(x2, y2);
            var map = InitializeMap(1, 1);
            var path = PathFinder.FindPaths(map, start, end);
            Assert.Null(path);
        }
        
        [TestCase(0, 0, 0, 0)]
        public void ZeroLengthPath(int x1, int y1, int x2, int y2)
        {
            var start = new Point(x1, y1);
            var end = new Point(x2, y2);
            var map = InitializeMap(1, 1);
            var path = PathFinder.FindPaths(map, start, end);
            Assert.AreEqual(end, path.Value);
            Assert.AreEqual(1, path.Length);
        }
        
        [TestCase(0, 0, 2, 2)]
        public void IsCorrectPath(int x1, int y1, int x2, int y2)
        {
            var start = new Point(x1, y1);
            var end = new Point(x2, y2);
            var map = InitializeMap(3, 3);
            var path = PathFinder.FindPaths(map, start, end);
            Assert.AreEqual(end, path.Value);
            Assert.AreEqual(5, path.Length);
        }

        // [TestCase(1, 1,
        //     0, 1,
        //     1, 0,
        //     2, 1,
        //     1, 2)]
        // public static void NeighboursTest(int x, int y, 
        //     int expX1, int expY1, 
        //     int expX2, int expY2, 
        //     int expX3, int expY3, 
        //     int expX4, int expY4)
        // {
        //     var map = InitializeMap(6, 6);
        //     var visited = new HashSet<Point>();
        //     var queue = new Queue<SinglyLinkedList<Point>>();
        //     var path = new SinglyLinkedList<Point>(new Point(x, y));
        //     PathFinder.AddNeighbours(map, queue, path, visited, new Point(x, y));
        //     
        //     Assert.AreEqual(,);
        // }
        
        [TestCase(-1, 0, false, 0, 0)]
        [TestCase(0, -1, false, 0, 0)]
        [TestCase(10, 0, false, 0, 0)]
        [TestCase(0, 10, false, 0, 0)]
        [TestCase(5, 5, false, 5, 5)]
        [TestCase(2, 2, true, 1, 2)]
        [TestCase(2, 2, true, 3, 2)]
        [TestCase(2, 2, true, 0, 0)]
        public static void IsPointAvailableTest(int x, int y, bool expected,
            int visitedX, int visitedY)
        {
            var map = InitializeMap(10, 10);
            var visited = new HashSet<Point> {new Point(visitedX, visitedY)};
            Assert.AreEqual(expected, PathFinder.IsPointAvailable(map, new Point (x, y), visited, new Point (0, 0)));
        }
        
        [TestCase(0, 0, true)]
        [TestCase(4, 4, true)]
        [TestCase(0, 4, true)]
        [TestCase(4, 0, true)]
        [TestCase(-1, 0, false)]
        [TestCase(0, -1, false)]
        [TestCase(0, 5, false)]
        [TestCase(5, 0, false)]
        public void InBoundsTests(int x, int y, bool expected)
        {
            var map = InitializeMap(5, 5);
            Assert.AreEqual(expected, PathFinder.InBounds(new Point (x, y), map, new Point (0, 0)));
        }
        
        private static GridState[,] InitializeMap(int x, int y)
        {
            var pathfinderGrid = new GridState [x, y];
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    pathfinderGrid[i,j] = GridState.Free;
                }
            }

            return pathfinderGrid;
        }
    }
}
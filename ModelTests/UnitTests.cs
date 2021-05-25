using System.Drawing;
using Model;
using NUnit.Framework;

namespace ModelTests
{
    public class PathFinderTest
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
            Assert.AreEqual(path.Value, end);
            Assert.AreEqual(path.Length, 2);
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
            Assert.AreEqual(path.Value, end);
            Assert.AreEqual(path.Length, 1);
        }
        
        [TestCase(0, 0, 2, 2)]
        public void IsCorrectPath(int x1, int y1, int x2, int y2)
        {
            var start = new Point(x1, y1);
            var end = new Point(x2, y2);
            var map = InitializeMap(3, 3);
            var path = PathFinder.FindPaths(map, start, end);
            Assert.AreEqual(path.Value, end);
            Assert.AreEqual(path.Length, 4);
        }
        
        //TODO написать тесты для остальных методов класса Pathfinder
        
        private static Map.State[,] InitializeMap(int x, int y)
        {
            var pathfinderGrid = new Map.State [x, y];
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    pathfinderGrid[i,j] = Map.State.Free;
                }
            }

            return pathfinderGrid;
        }
    }
}
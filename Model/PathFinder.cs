using System.Collections.Generic;
using System.Drawing;

namespace Model
{
    public class PathFinder
    {
        public static SinglyLinkedList<Point> FindPaths(Map.Availability[,] map, Point start, Point player)
        {
            var visited = new HashSet<Point>();
            var queue = new Queue<SinglyLinkedList<Point>>();
            queue.Enqueue(new SinglyLinkedList<Point>(start));
            visited.Add(start);
            
            while (queue.Count != 0)
            {
                var path = queue.Dequeue();
                var cell = path.Value;
                if (player == cell)
                    return path;

                AddNeighbours(map, queue, path, visited, cell);
            }

            return null;
        }

        private static void AddNeighbours(Map.Availability[,] map, Queue<SinglyLinkedList<Point>> queue, 
            SinglyLinkedList<Point> path, HashSet<Point> visited, Point cell)
        {
            for (var dy = -1; dy <= 1; dy++)
            {
                for (var dx = -1; dx <= 1; dx++)
                {
                    var neighbour = new Point {X = cell.X + dx, Y = cell.Y + dy};
                    if ((dx == 0 || dy == 0) && IsPointAvailable(map, neighbour, visited))
                    {
                        queue.Enqueue(new SinglyLinkedList<Point>(neighbour, path));
                        visited.Add(neighbour);
                    }
                }  
            } 
        }

        private static bool IsPointAvailable(Map.Availability[,] map, Point point, HashSet<Point> visited)
        {
            return !visited.Contains(point) && map[point.X, point.Y] != Map.Availability.Available;
        }
    }
}
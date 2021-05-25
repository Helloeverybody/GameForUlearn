using System.Collections.Generic;
using System.Drawing;

namespace Model
{
    public class PathFinder
    {
        public static SinglyLinkedList<Point> FindPaths(Map.State[,] map, Point start, Point player)
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

        public static void AddNeighbours(Map.State[,] map, Queue<SinglyLinkedList<Point>> queue, 
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

        public static bool IsPointAvailable(Map.State[,] map, Point point, HashSet<Point> visited)
        {
            return InBounds(point, map) && !visited.Contains(point) && map[point.X,point.Y] != Map.State.Free;
        }
        
        private static bool InBounds(Point point, Map.State[,] map)
        {
            return point.X > 0 && point.X < map.GetLength(0) && 
                   point.Y > 0 && point.Y < map.GetLength(1);
        }
    }
}
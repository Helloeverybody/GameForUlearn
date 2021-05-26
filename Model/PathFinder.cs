using System;
using System.Collections.Generic;
using System.Drawing;

namespace Model
{
    public class PathFinder
    {
        public static SinglyLinkedList<Point> FindPaths(GridState[,] map, Point start, Point end)
        {
            var visited = new HashSet<Point>();
            var queue = new Queue<SinglyLinkedList<Point>>();
            queue.Enqueue(new SinglyLinkedList<Point>(start));
            visited.Add(start);
            
            while (queue.Count != 0)
            {
                var path = queue.Dequeue();
                var cell = path.Value;
                if (end == cell)
                    return path;

                AddNeighbours(map, queue, path, visited, cell, end);
            }
            return null;
        }

        public static void AddNeighbours(GridState[,] map, Queue<SinglyLinkedList<Point>> queue, 
            SinglyLinkedList<Point> path, HashSet<Point> visited, Point cell, Point monster)
        {
            for (var dy = -1; dy <= 1; dy++)
            {
                for (var dx = -1; dx <= 1; dx++)
                {
                    var neighbour = new Point {X = cell.X + dx, Y = cell.Y + dy};
                    if (!(dx == 0 && dy == 0) && IsPointAvailable(map, neighbour, visited, monster))
                    {
                        queue.Enqueue(new SinglyLinkedList<Point>(neighbour, path));
                        visited.Add(neighbour);
                    }
                }  
            } 
        }

        public static bool IsPointAvailable(GridState[,] map, Point point, HashSet<Point> visited, Point monster)
        {
            return InBounds(point, map, monster) && !visited.Contains(point) && map[point.X,point.Y] == GridState.Free;
        }
        
        public static bool InBounds(Point point, GridState[,] map, Point monster)
        {
            return point.X >= 0 && point.X < map.GetLength(0)&& 
                   point.Y >= 0 && point.Y < map.GetLength(1);
        }
    }
}
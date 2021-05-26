using System;
using System.Drawing;

namespace Model
{
    public class Monster
    {
        public int X;
        public int Y;
        public readonly Bitmap Sprite;
        public SinglyLinkedList<Point> Path { get; set; }

        public Monster(int x, int y)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Monster.png";
            Sprite = (Bitmap)Image.FromFile(path);
            X = x;
            Y = y;
        }
        
        public SinglyLinkedList<Point> Move(SinglyLinkedList<Point> path, int gridScale)
        {
            if (path == null)
                return null;
            X = path.Value.X * gridScale;
            Y = path.Value.Y * gridScale;
            return path.Previous;
        }
        
        public bool IsNearby(float x, float y)
        {
            return Math.Sqrt((x - X) * (x - X) + (y - Y) * (y - Y)) <= 300;
        }
    }
}
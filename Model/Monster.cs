using System;
using System.Drawing;

namespace Model
{
    public class Monster
    {
        public int X;
        public int Y;

        public Bitmap Sprite;

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
    }
}
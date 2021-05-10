using System;
using System.Drawing;

namespace Model
{
    public class OnMapItem
    {
        public string Name;
        public int Weight;
        public Image ItemSprite;
        
        public int X;
        public int Y;

        public OnMapItem(string name, int x, int y)
        {
            Name = name;
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Item.png";
            ItemSprite = Image.FromFile(path);
            X = x;
            Y = y;
        }

        public bool IsNearby(int x, int y)
        {
            return Math.Sqrt((x - X) * (x - X) + (y - Y) * (y - Y)) <= 50;
        }
    }
}
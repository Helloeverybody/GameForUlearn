using System;
using System.Drawing;

namespace Model
{
    public class OnMapItem
    {
        public string Name;
        public int Weight;
        public Image ItemSprite;
        
        public bool IsDialogable;
        public bool IsPickable;
        
        public int X;
        public int Y;

        public OnMapItem(string name, int x, int y, int weight)
        {
            Name = name;
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Item.png";
            ItemSprite = Image.FromFile(path);
            X = x;
            Y = y;
            Weight = weight;
        }
        
        public bool IsNearby(int x, int y)
        {
            return Math.Sqrt((x - X * 2) * (x - X * 2) + (y - Y * 2) * (y - Y * 2)) <= 70;
        }

        public void ToInventory()
        {
            
        }
    }
}
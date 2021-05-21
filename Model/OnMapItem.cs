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

        public OnMapItem(string name, int x, int y, int weight, bool isDialogable, bool isPickable)
        {
            Name = name;
            Weight = weight;
            
            X = x;
            Y = y;

            IsDialogable = isDialogable;
            IsPickable = isPickable;
            
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\woodLog.png";
            ItemSprite = Image.FromFile(path);
        }
        
        public bool IsNearby(float x, float y)
        {
            return Math.Sqrt((x - X * 2) * (x - X * 2) + (y - Y * 2) * (y - Y * 2)) <= 70;
        }

        public void ToInventory()
        {
            
        }
    }
}
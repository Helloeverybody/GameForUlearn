using System;
using System.Drawing;

namespace Model
{
    public class OnMapItem
    {
        public string Name;
        public int Weight;
        public Image Sprite;
        public bool IsDialogable;
        public bool IsPickable;
        public int X;
        public int Y;
        public bool IsNearby;
        public bool IsHighlighted;

        public OnMapItem(string name, int x, int y, int weight, bool isDialogable, bool isPickable)
        {
            Name = name;
            Weight = weight;
            X = x;
            Y = y;
            IsDialogable = isDialogable;
            IsPickable = isPickable;
            IsHighlighted = false;
            
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\woodLog.png";
            Sprite = Image.FromFile(path);
        }
        
        public bool CheckIsNearby(float x, float y)
        {
            var isNearby = Math.Sqrt((x - X) * (x - X) + (y - Y) * (y - Y)) <= 100;
            IsNearby = isNearby;
            return isNearby;
        }
    }
}
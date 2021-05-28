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

        public bool IsHighlighed;

        public PointF OnMapCoordinates;

        public OnMapItem(string name, int x, int y, int weight, bool isDialogable, bool isPickable)
        {
            Name = name;
            Weight = weight;
            
            X = x;
            Y = y;
            OnMapCoordinates = new PointF(x, y);

            IsDialogable = isDialogable;
            IsPickable = isPickable;

            IsHighlighed = false;
            
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\woodLog.png";
            ItemSprite = Image.FromFile(path);
        }
        
        public bool IsNearby(float x, float y)
        {
            var isNearby = Math.Sqrt((x - X) * (x - X) + (y - Y) * (y - Y)) <= 100;
            IsHighlighed = isNearby;
            return isNearby;
        }

        public void ToInventory()
        {
            
        }
    }
}
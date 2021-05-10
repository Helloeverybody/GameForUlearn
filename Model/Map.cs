using System;
using System.Drawing;
using System.Windows.Forms;

namespace Model
{
    public class Map
    {
        public Bitmap mapSprite;
        public Bitmap currentDistrict;
        public Point Anchor;
        public Map()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Map.png";
            mapSprite = (Bitmap)Image.FromFile(path);
            Anchor = new Point(0, 0);
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace My_game_for_Ulearn.Model
{
    public class Map
    {
        public Bitmap mapSprite;
        public Bitmap currentDistrict;
        public Point Anchor;
        public Map(int width, int height)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Map.png";
            mapSprite = (Bitmap)Image.FromFile(path);
            Anchor = new Point(0, 0);
        }

        public void RevaluateDistrict(int width, int height)
        {
            var newDis = new Bitmap(width, height);
            for (var i = 0; i < newDis.Width; i++)
                for (var j = 0; j < newDis.Height; j++)
                    newDis.SetPixel(i, j, mapSprite.GetPixel(i + Anchor.X, j + Anchor.Y));

            currentDistrict = newDis;
        }
    }
}
using System;
using System.Drawing;

namespace Model
{
    public class Scale
    {
        public int maxValue = 100;
        public int minValue = 0;
        public int Value;
        public Bitmap Sprite;
        
        public Scale()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\HUD\Character\Heal_1.png";
            Sprite = (Bitmap)Image.FromFile(path);
            Value = minValue;
        }
    }
}
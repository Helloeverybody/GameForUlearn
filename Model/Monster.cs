using System;
using System.Drawing;

namespace Model
{
    public class Monster
    {
        public float X;
        public float Y;

        public Bitmap Sprite;

        public Monster()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Monster.png";
            Sprite = (Bitmap)Image.FromFile(path);
        }
        
        public void MonsterMove()
        {
            
        }
    }
}
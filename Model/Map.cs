using System;
using System.Drawing;
using System.Windows.Forms;

namespace Model
{
    public class Map
    {
        public PointF Anchor;
        public Bitmap mapSprite;
        public Vector Move = Vector.Zero;
        
        float PlayerSpeed = 2.4f;
        
        public Map(Size size)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Map.png";
            mapSprite = (Bitmap)Image.FromFile(path);
            Anchor = new PointF(size.Width, size.Height);
        }
        
        public bool MoveBack = false;
        public bool MoveForward = false;
        public bool MoveLeft = false;
        public bool MoveRight = false;
        
        public void Translate()
        {
            if (MoveForward && MoveLeft)
                Move += new Vector(-0.7f, -0.7f);
            else if (MoveForward && MoveRight)
                Move += new Vector(0.7f, -0.7f);
            else if (MoveBack && MoveLeft)
                Move += new Vector(-0.7f, 0.7f);
            else if (MoveBack && MoveRight )
                Move += new Vector(0.7f, 0.7f);
            else if (MoveForward)
                Move += new Vector(0, -1);
            else if (MoveBack)
                Move += new Vector(0, 1);
            else if (MoveRight)
                Move += new Vector(1, 0);
            else if (MoveLeft)
                Move += new Vector(-1, 0);

            Anchor.X = PlayerSpeed * Move.X;
            Anchor.Y = PlayerSpeed * Move.Y;
        }
    }
}
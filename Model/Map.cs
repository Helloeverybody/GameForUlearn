using System;
using System.Drawing;
using System.Windows.Forms;

namespace Model
{
    public class Map
    {
        public Bitmap mapSprite;
        public PointF Anchor;
        
        //public double speed;
        private const float MaxSpeed = 2.4f;
        public Vector Move = Vector.Zero;
        
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

            Anchor.X = MaxSpeed * Move.X;
            Anchor.Y = MaxSpeed * Move.Y;
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace My_game_for_Ulearn.Model
{
    public class Player
    {
        public float X;
        public float Y;
        public Image playerSprite;
        private const float maxSpeed = 3;
        public Vector move = Vector.Zero;
        public double speed;

        public bool WalkForward = false;
        public bool WalkBackward = false;
        public bool WalkRight = false;
        public bool WalkLeft = false;
        
        public Player(float x, float y)
        {
            X = x;
            Y = y;
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Player.png";
            playerSprite = Image.FromFile(path);
        }
        
        public Player(PointF point)
        {
            X = point.X;
            Y = point.X;
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Player.png";
            playerSprite = Image.FromFile(path);
        }
        
        public void Translate()
        {
            if (WalkForward)
                move += new Vector(0, -1);
            if (WalkBackward)
                move += new Vector(0, 1);
            if (WalkRight)
                move += new Vector(1, 0);
            if (WalkLeft)
                move += new Vector(-1, 0);

            X = maxSpeed * move.X;
            Y = maxSpeed * move.Y;
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Model
{
    public class Player
    {
        public int X;
        public int Y;
        public Image playerSprite;
        private const int maxSpeed = 3;
        public Vector move = Vector.Zero;
        public double speed;

        public bool WalkForward = false;
        public bool WalkBackward = false;
        public bool WalkRight = false;
        public bool WalkLeft = false;
        
        public Player(int x, int y)
        {
            X = x;
            Y = y;
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Player.png";
            playerSprite = Image.FromFile(path);
        }
        
        public Player(Point point)
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
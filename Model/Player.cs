using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace Model
{
    public class Player
    {
        public int X;
        public int Y;
        public Image playerSprite;
        private const int maxSpeed = 1;
        public Vector move = Vector.Zero;
        //public double speed;

        public bool WalkForward = false;
        public bool WalkBack = false;
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
            if (!(WalkBack || WalkForward || WalkLeft || WalkRight))
                move = Vector.Zero;
            else if (WalkForward && WalkRight)
                move = new Vector(4, -4) * (int) Math.Sqrt(2);
            else if (WalkForward && WalkLeft)
                move = new Vector(-4, -4) * (int) Math.Sqrt(2);
            else if (WalkBack && WalkRight)
                move = new Vector(4, 4) * (int) Math.Sqrt(2);
            else if (WalkBack && WalkLeft)
                move = new Vector(-4, 4) * (int) Math.Sqrt(2);
            else if (WalkForward)
                move = new Vector(0, -5);
            else if (WalkBack)
                move = new Vector(0, 5);
            else if (WalkRight)
                move = new Vector(5, 0);
            else if (WalkLeft)
                move = new Vector(-5, 0);
            
            X += maxSpeed * move.X;
            Y += maxSpeed * move.Y;
        }
    }
}
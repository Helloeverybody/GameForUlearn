using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace Model
{
    public class Player
    {
        public int X;
        public int Y;
        public Image playerSprite;
        
        //public double speed;
        private const int maxSpeed = 1;
        public Vector move = Vector.Zero;
        
        public Inventory Inventory { get; set; }

        // public List<OnMapItem> NearbyItems
        // {
        //     get => items.Where(item => item.IsNearby(X, Y)).ToList();
        // }

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
            if (WalkForward && WalkLeft)
                move += new Vector(-7, -7);
            else if (WalkForward && WalkRight)
                move += new Vector(7, -7);
            else if (WalkBack && WalkLeft)
                move += new Vector(-7, 7);
            else if (WalkBack && WalkRight)
                move += new Vector(7, 7);
            else if (WalkForward)
                move += new Vector(0, -10);
            else if (WalkBack)
                move += new Vector(0, 10);
            else if (WalkRight)
                move += new Vector(10, 0);
            else if (WalkLeft)
                move += new Vector(-10, 0);

            X = maxSpeed * move.X;
            Y = maxSpeed * move.Y;
        }
        
        public List<OnMapItem> NearbyItems(IEnumerable<OnMapItem> items)
        {
            return items.Where(item => item.IsNearby(X, Y)).ToList();
        }
    }
}
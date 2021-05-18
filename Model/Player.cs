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
        
        public Inventory Inventory { get; set; }

        // public List<OnMapItem> NearbyItems
        // {
        //     get => items.Where(item => item.IsNearby(X, Y)).ToList();
        // }

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

        public List<OnMapItem> NearbyItems(IEnumerable<OnMapItem> items)
        {
            return items.Where(item => item.IsNearby(X, Y)).ToList();
        }
    }
}
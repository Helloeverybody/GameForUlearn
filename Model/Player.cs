using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
namespace Model
{
    public class Player
    {
        public int X;
        public int Y;
        public Image Sprite;
        
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
            Sprite = Image.FromFile(path);
        }

        public List<OnMapItem> NearbyItems(IEnumerable<OnMapItem> items, PointF anchor)
        {
            return items.Where(item => item.IsNearby(anchor.X + X, anchor.Y + Y)).ToList();
        }

        public void MovePlayer(Map map)
        {
            map.UpdateMap(this);
            map.Translate();
        }
    }
}
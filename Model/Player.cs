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
        public Scale MadnessScale;
        private readonly float playerSpeed = 4f;

        // public List<OnMapItem> NearbyItems
        // {
        //     get => items.Where(item => item.IsNearby(X, Y)).ToList();
        // }

        public Player(int x, int y)
        {
            X = x;
            Y = y;

            MadnessScale = new Scale();
            Inventory = new Inventory();
            
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Player.png";
            Sprite = Image.FromFile(path);
        }

        public List<OnMapItem> NearbyItems(IEnumerable<OnMapItem> items, PointF anchor)
        {
            return items.Where(item => item.IsNearby(anchor.X + X, anchor.Y + Y)).ToList();
        }
        
        public bool NearbyMonsters(Monster monster, PointF anchor)
        {
            return monster.IsNearby(anchor.X + X, anchor.Y + Y);
        }
        
        public bool Damaging(Monster monster, PointF anchor)
        {
            return Math.Sqrt((anchor.X + X - monster.X) * (anchor.X + X - monster.X) + 
                             (anchor.Y + Y - monster.Y) * (anchor.Y + Y - monster.Y)) <= 200;
        }

        public void MovePlayer(Map map)
        {
            map.UpdateMap(this);
            map.Translate(playerSpeed);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Model
{
    public class Map
    {
        public PointF Anchor;
        public Bitmap Sprite;
        
        public PointF Move;
        public Availability[,] AccessMap;
        public List<OnMapItem> ItemsOnMap { get; set; }
        public List<OnMapItem> ItemsNearPlayer { get; set; } = new List<OnMapItem>();

        private readonly float PlayerSpeed = 2.5f;
        
        public bool MoveBack = false;
        public bool MoveForward = false;
        public bool MoveLeft = false;
        public bool MoveRight = false;
        
        public Map(Size size)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\TestMap.png";
            Move = new PointF(0, 0);
            Anchor = new PointF(size.Width, size.Height);
            Sprite = (Bitmap)Image.FromFile(path);
            ItemsOnMap = new List<OnMapItem>();
        }
        
        public enum Availability
        {
            Available,
            Unavailable
        }
        
        public void Translate()
        {
            if (MoveForward && MoveLeft)
                Move = new PointF(Move.X - 0.7f, Move.Y - 0.7f);
            else if (MoveForward && MoveRight)
                Move = new PointF(Move.X + 0.7f, Move.Y -0.7f);
            else if (MoveBack && MoveLeft)
                Move = new PointF(Move.X - 0.7f, Move.Y + 0.7f);
            else if (MoveBack && MoveRight )
                Move = new PointF(Move.X + 0.7f, Move.Y + 0.7f);
            else if (MoveForward)
                Move = new PointF(Move.X + 0, Move.Y-1);
            else if (MoveBack)
                Move = new PointF(Move.X + 0, Move.Y + 1);
            else if (MoveRight)
                Move = new PointF(Move.X + 1, Move.Y + 0);
            else if (MoveLeft)
                Move = new PointF(Move.X - 1, Move.Y + 0);

            Anchor.X = PlayerSpeed * Move.X;
            Anchor.Y = PlayerSpeed * Move.Y;
        }

        public PointF GetOnMapCoordinates(float x, float y)
        {
            return new PointF(Anchor.X - x, Anchor.Y - y);
        }
        
        public void AddItemsOnMap()
        {
            ItemsOnMap.Add(new OnMapItem("testItem", 20, 20, 10, false, true));
            //ItemsOnMap.Add(new OnMapItem("testItem", 100, 20, 10, false, true));
            //ItemsOnMap.Add(new OnMapItem("testItem", 180, 20, 10, true, false));
        }

        public void UpdateMap(Player player)
        {
            ItemsNearPlayer = player.NearbyItems(ItemsOnMap, Anchor);
            foreach (var item in ItemsOnMap)
            {
                item.OnMapCoordinates = GetOnMapCoordinates(item.X, item.Y);
            }
        }

        public void PickUpItem(Inventory inventory)
        {
            var item = ItemsNearPlayer.First();
            //TODO выдает эксепшн, поправить
            //inventory.Add(item);
            ItemsOnMap.Remove(item);
        }
    }
}
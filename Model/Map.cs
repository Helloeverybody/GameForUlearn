using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Model
{
    public class Map
    {
        public PointF Anchor;
        public Bitmap Sprite;
        private PointF move;
        public GridState[,] PathfinderGrid;
        public readonly int GridScale;
        
        public bool MoveBack = false;
        public bool MoveForward = false;
        public bool MoveLeft = false;
        public bool MoveRight = false;
        
        public List<OnMapItem> ItemsOnMap { get; set; }
        public List<OnMapItem> ItemsNearPlayer { get; set; } 
        
        public Bitmap MapSprite;

        private Size formSize;
        
        public Map(Size size)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Map.png";
            move = new PointF(0, 0);
            Anchor = new PointF(0, 0);
            Sprite = (Bitmap)Image.FromFile(path);
            GridScale = 4;
            ItemsOnMap = new List<OnMapItem>();
            ItemsNearPlayer = new List<OnMapItem>();
            MapSprite = new Bitmap(Sprite.Width, Sprite.Height);
            formSize = size;
            
            AddItemsOnMap();
            InitializeGrid();
        }
        
        public void RedrawMap()
        {
            var g = Graphics.FromImage(MapSprite);
            var rect = new Rectangle(new Point(0, 0), new Size(MapSprite.Size.Width, MapSprite.Size.Height));
            
            g.DrawImage(Sprite, rect, Anchor.X, Anchor.Y, MapSprite.Size.Width, MapSprite.Size.Height, GraphicsUnit.Pixel);
            
            foreach (var item in ItemsOnMap)
            {
                g.DrawImage(item.ItemSprite, rect, item.OnMapCoordinates.X, item.OnMapCoordinates.Y,
                    MapSprite.Size.Width, MapSprite.Size.Height, GraphicsUnit.Pixel);
            }
            
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\eIcon.png";
            var eIcon = (Bitmap)Image.FromFile(path);

            foreach (var item in ItemsNearPlayer)
            {
                g.DrawImage(eIcon, rect, item.X, item.Y, 
                    MapSprite.Size.Width, MapSprite.Size.Height, GraphicsUnit.Pixel);
            }
        }
        
        public void InitializeGrid()
        {
            PathfinderGrid = new GridState [Sprite.Size.Width / GridScale, Sprite.Size.Height / GridScale];
            
            for (var i = 0; i < PathfinderGrid.GetLength(0); i++)
                for (var j = 0; j < PathfinderGrid.GetLength(1); j++)
                    PathfinderGrid[i,j] = GridState.Free;
        }  
        
        public void Translate(float playerSpeed)
        {
            if (MoveForward && MoveLeft)
                move = new PointF(move.X - 0.7f, move.Y - 0.7f);
            else if (MoveForward && MoveRight)
                move = new PointF(move.X + 0.7f, move.Y -0.7f);
            else if (MoveBack && MoveLeft)
                move = new PointF(move.X - 0.7f, move.Y + 0.7f);
            else if (MoveBack && MoveRight )
                move = new PointF(move.X + 0.7f, move.Y + 0.7f);
            else if (MoveForward)
                move = new PointF(move.X + 0, move.Y-1);
            else if (MoveBack)
                move = new PointF(move.X + 0, move.Y + 1);
            else if (MoveRight)
                move = new PointF(move.X + 1, move.Y + 0);
            else if (MoveLeft)
                move = new PointF(move.X - 1, move.Y + 0);

            Anchor.X = playerSpeed * move.X;
            Anchor.Y = playerSpeed * move.Y;
        }

        public PointF GetOnMapCoordinates(float x, float y)
        {
            return new PointF(Anchor.X - x, Anchor.Y - y);
        }
        
        public void AddItemsOnMap()
        {
            ItemsOnMap.Add(new OnMapItem("testItem", 10, 10, 10, false, true));
            ItemsOnMap.Add(new OnMapItem("testItem", -50, -50, 10, false, true));
            ItemsOnMap.Add(new OnMapItem("testItem", -100, -100, 10, false, true));
        }

        public void UpdateMap(Player player)
        {
            ItemsNearPlayer = player.NearbyItems(ItemsOnMap, Anchor);
            foreach (var item in ItemsOnMap)
                item.OnMapCoordinates = GetOnMapCoordinates(item.X, item.Y);
        }

        public void PickUpItem(Inventory inventory)
        {
            var item = ItemsNearPlayer.First();
            inventory.Add(item);
            ItemsOnMap.Remove(item);
            RedrawMap();
        }
    }
}
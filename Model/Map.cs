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
        private Bitmap groundSprite;
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
        
        public Map(Size size)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Map.png";
            move = new PointF(0, 0);
            Anchor = new PointF(0, 0);
            groundSprite = (Bitmap)Image.FromFile(path);
            GridScale = 4;
            ItemsOnMap = new List<OnMapItem>();
            ItemsNearPlayer = new List<OnMapItem>();
            MapSprite = new Bitmap(groundSprite.Width, groundSprite.Height);
            
            AddItemsOnMap();
            InitializeGrid();
        }
        
        public void RedrawMap()
        {
            var g = Graphics.FromImage(MapSprite);
            var rect = new Rectangle(new Point(0, 0), new Size(MapSprite.Size.Width, MapSprite.Size.Height));
            
            g.DrawImage(groundSprite, rect, 0, 0, MapSprite.Size.Width, MapSprite.Size.Height, GraphicsUnit.Pixel);
            
            foreach (var item in ItemsOnMap)
            {
                var itemRect = new Rectangle(new Point(item.X - item.Sprite.Width / 2,
                    item.Y - item.Sprite.Height / 2), new Size(item.Sprite.Width, item.Sprite.Height));
                g.DrawImage(item.Sprite, itemRect, 0, 0, 100, 100, GraphicsUnit.Pixel);
            }
            
            // ПАМЯТКА ПО DrawImage (потому что документация говно)
            //
            // Ректангл                  - задает размер и положение прямоугольника, в котором рисуется изображение
            // Поинт в ректангле         - задает расположение верхнего левого угла ректангла
            // Сайз в ректангле          - задает размер ректангла
            // ширина-высота в DrawImage - задает ширину-высоту картинки, которая рисуется в ректангле
            // координаты в DrawImage    - задают отрицательное смещение относительно верхнего левого угла ректангла
            
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\eIcon.png";
            var eIcon = (Bitmap)Image.FromFile(path);

            foreach (var item in ItemsNearPlayer)
            {
                var emblemRect = new Rectangle(new Point(item.X - eIcon.Width / 2,
                    item.Y - eIcon.Height / 2 - 40), new Size(eIcon.Width, eIcon.Height));
                g.DrawImage(eIcon, emblemRect, 0, 0, 100, 100, GraphicsUnit.Pixel);
            }
        }

        // в разработке ////////////////////////////////////////////////////////////////////////////////////////////////
        // public void EraseItem(OnMapItem item)
        // {
        //     var g = Graphics.FromImage(MapSprite);
        //     var rect = new Rectangle(new Point(item.X - item.Sprite.Width / 2, 
        //         item.Y - item.Sprite.Height / 2), new Size(100, 100));
        //     g.DrawImage(MapSprite, rect, 0, 0, 100, 100, GraphicsUnit.Pixel);
        // }
        //
        // public void DrawItem(OnMapItem item)
        // {
        //     var g = Graphics.FromImage(MapSprite);
        //     var itemRect = new Rectangle(new Point(item.X - item.Sprite.Width / 2, 
        //         item.Y - item.Sprite.Height / 2), new Size(100, 100));
        //     g.DrawImage(item.Sprite, itemRect, 0, 0, 100, 100, GraphicsUnit.Pixel);
        // }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private void InitializeGrid()
        {
            PathfinderGrid = new GridState [groundSprite.Size.Width / GridScale, groundSprite.Size.Height / GridScale];
            
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
        
        public void AddItemsOnMap()
        {
            ItemsOnMap.Add(new OnMapItem("testItem", 200, 100, 10, false, true));
            ItemsOnMap.Add(new OnMapItem("testItem", 400, 100, 10, false, true));
            ItemsOnMap.Add(new OnMapItem("testItem", 600, 100, 10, false, true));
        }

        public void UpdateMap(Player player)
        {
            ItemsNearPlayer = player.NearbyItems(ItemsOnMap, Anchor);
        }

        public void PickUpItem(Player player)
        {
            var item = ItemsNearPlayer.First();
            if (!player.Inventory.Add(item)) return;
            ItemsOnMap.Remove(item);
            UpdateMap(player);
            RedrawMap();
        }
    }
}
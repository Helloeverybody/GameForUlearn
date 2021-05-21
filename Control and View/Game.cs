using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Model;

namespace My_game_for_Ulearn
{
    public class Game : UserControl
    {
        private MainForm mainForm;
        private IContainer components;
        
        public Map Map { get; set; }
        public Player Player { get; set; }
        public Timer PaintTimer { get; }
        public Timer WalkTimer { get; }
        public List<OnMapItem> ItemsOnMap { get; set; }
        
        public Game(MainForm form)
        {
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            mainForm = form;
            
            Player = new Player(Size.Width / 2, Size.Height / 2);
            Map = new Map(Size);
            
            // TODO оно лагает все равно, нужно пофиксить
            WalkTimer = new Timer { Interval = 10 };
            WalkTimer.Start();
            WalkTimer.Tick += OnWalkTick;
            
            PaintTimer = new Timer { Interval = 10 };
            PaintTimer.Start();
            PaintTimer.Tick += OnTick;
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();

            ItemsOnMap = new List<OnMapItem>();
            AddItemsOnMap();
        }

        public void AddItemsOnMap()
        {
            ItemsOnMap.Add(new OnMapItem("testItem", 20, 20, 10, true, true));
            ItemsOnMap.Add(new OnMapItem("testItem", 100, 20, 10, false, true));
            ItemsOnMap.Add(new OnMapItem("testItem", 180, 20, 10, true, false));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            
            var rect = new Rectangle(new Point(0, 0), Size);
            
            g.DrawImage(Map.MapSprite, rect, Map.Anchor.X, Map.Anchor.Y, Size.Width, Size.Height, GraphicsUnit.Point);

            foreach (var item in ItemsOnMap)
            {
                var itemCoords = Map.GetOnMapCoordinates(item.X, item.Y);
                g.DrawImage(item.ItemSprite, rect, itemCoords.X, itemCoords.Y,
                    Size.Width, Size.Height, GraphicsUnit.Point);
            }
                
            g.DrawImage(Player.playerSprite, Player.X - 25, Player.Y - 25, 50,50);
            
            //TODO кажется, слишком долго просчитывает, отрисовка начинает тормозить, поправить
            var nearbyItems = Player.NearbyItems(ItemsOnMap, Map.Anchor);
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\eIcon.png";
            var eIcon = (Bitmap)Image.FromFile(path);
            
            //TODO опять неправильно высчитывает координаты 
            foreach (var itemCoords in nearbyItems.Select(item => Map.GetOnMapCoordinates(item.X, item.Y - 15)))
            {
                g.DrawImage(eIcon, rect, itemCoords.X, itemCoords.Y,
                    Size.Width, Size.Height, GraphicsUnit.Point);
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            Refresh(); 
        }
        
        private void OnWalkTick(object sender, EventArgs e)
        {
            Map.Translate();
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            HandleKey(e.KeyCode, true);
            
            if (e.KeyCode == Keys.E)
            {
                //TODO может собрать несобираемые предметы,если рядом лежат собираемые
                if (Player.NearbyItems(ItemsOnMap, Map.Anchor).Count(x => x.IsDialogable) != 0)
                {
                    mainForm.StartDialog();
                } 
                
                if (Player.NearbyItems(ItemsOnMap, Map.Anchor).Count(x => x.IsPickable) != 0)
                {
                    foreach (var item in Player.NearbyItems(ItemsOnMap, Map.Anchor))
                    {
                        //TODO выдает эксепшн, поправить
                        //Player.Inventory.Add(item);
                        ItemsOnMap.Remove(item);
                    }
                } 

                var dialog = new Model.Dialog("Это тестовоый диалог.");
            }

            if (e.KeyCode == Keys.Escape)
            {
                mainForm.PauseGame();
            }

            if (e.KeyCode == Keys.F)
            {
                mainForm.OpenInventory();
            }
        }
        
        protected override void OnKeyUp(KeyEventArgs e)
        {
            HandleKey(e.KeyCode, false);
        }
        
        private void HandleKey(Keys key, bool value)
        {
            if (key == Keys.A || key == Keys.Left) Map.MoveLeft = value;
            if (key == Keys.D || key == Keys.Right) Map.MoveRight = value;
            if (key == Keys.W || key == Keys.Up) Map.MoveForward = value;
            if (key == Keys.S || key == Keys.Down) Map.MoveBack = value;
        }
    }
}
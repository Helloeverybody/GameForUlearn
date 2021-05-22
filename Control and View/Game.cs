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
        public Timer Timer { get; }

        public Game(MainForm form)
        {
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            mainForm = form;
            
            Player = new Player(Size.Width / 2, Size.Height / 2);
            Map = new Map(Size);
            
            // TODO оно лагает все равно, нужно пофиксить
            Timer = new Timer { Interval = 10 };
            Timer.Start();
            Timer.Tick += OnTick;
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();

            Map.AddItemsOnMap();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            
            var rect = new Rectangle(new Point(0, 0), Size);
            
            g.DrawImage(Map.Sprite, rect, Map.Anchor.X, Map.Anchor.Y, Size.Width, Size.Height, GraphicsUnit.Pixel);

            foreach (var item in Map.ItemsOnMap)
            {
                g.DrawImage(item.ItemSprite, rect, item.OnMapCoordinates.X, item.OnMapCoordinates.Y,
                    Size.Width, Size.Height, GraphicsUnit.Pixel);
            }

            g.DrawImage(Player.Sprite, Player.X - Player.Sprite.Width / 2, Player.Y - Player.Sprite.Height / 2);
            
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\eIcon.png";
            var eIcon = (Bitmap)Image.FromFile(path);
            
            //TODO кажется, слишком долго просчитывает, отрисовка начинает тормозить, поправить
            //TODO опять неправильно высчитывает координаты 
            foreach (var itemCoords in Map.ItemsNearPlayer.Select(item => Map.GetOnMapCoordinates(item.X, item.Y - 40)))
            {
                g.DrawImage(eIcon, rect, itemCoords.X, itemCoords.Y,
                    Size.Width, Size.Height, GraphicsUnit.Pixel);
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            Map.UpdateMap(Player);
            Map.Translate();
            Refresh(); 
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            HandleKey(e.KeyCode, true);
            
            if (e.KeyCode == Keys.E)
            {
                if (Map.ItemsNearPlayer.Count(x => x.IsDialogable) != 0)
                {
                    mainForm.StartDialog();
                } 
                
                if (Map.ItemsNearPlayer.Count(x => x.IsPickable) != 0)
                {
                    Map.PickUpItem(Player.Inventory);
                }
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
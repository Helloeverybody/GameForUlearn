using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
        public List<OnMapItem> ItemsOnMap { get; set; }
        
        public Game(MainForm form)
        {
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            Name = "Game";
            mainForm = form;
            
            Player = new Player(Size.Width / 2, Size.Height / 2);
            Map = new Map(Size);
            
            Timer = new Timer { Interval = 1 };
            Timer.Start();
            Timer.Tick += OnTick;
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
            
            ItemsOnMap = new List<OnMapItem>();
            ItemsOnMap.Add(new OnMapItem("testItem", Size.Width / 2, Size.Height / 2, 10));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var size = new Size(Size.Width * 2, Size.Height * 2);
            var rect = new Rectangle(new Point(0, 0), size);
            
            g.DrawImage(Map.MapSprite, rect, Map.Anchor.X, Map.Anchor.Y, Size.Width, Size.Height, GraphicsUnit.Pixel);
            g.DrawImage(Player.playerSprite, Size.Width / 2 - 25, Size.Height / 2 - 25, 50,50);
            
            foreach (var item in ItemsOnMap)
                g.DrawImage(item.ItemSprite, rect, 0, 0, Size.Width, Size.Height, GraphicsUnit.Point);
            
            var nearbyItems = Player.NearbyItems(ItemsOnMap);
            foreach (var item in nearbyItems)
                g.DrawImage(Player.playerSprite, rect, 0, 0, Size.Width, Size.Height, GraphicsUnit.Point);
        }
        
        private void OnTick(object sender, EventArgs e)
        {
            Map.Translate();
            Refresh(); 
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            HandleKey(e.KeyCode, true);
            
            if (e.KeyCode == Keys.E)
            {
                if (mainForm.Dialog.Enabled != true /*&& Player.NearbyItems(itemsOnMap).Count(x => x.IsDialogable) != 0*/)
                {
                    mainForm.Game.Enabled = false;
                    mainForm.Dialog.Enabled = true;
                    mainForm.Dialog.Show();
                } 
                else
                {
                    mainForm.Dialog.Enabled = false;
                    mainForm.Game.Enabled = true;
                    mainForm.Game.Show();
                }
                    
                var dialog = new Model.Dialog("Это тестовоый диалог.");
                
                mainForm.Dialog.Enabled = true;
                
            }

            if (e.KeyCode == Keys.Escape)
            { 
                //GameMenu;
            }

            if (e.KeyCode == Keys.F)
            {
                //Inventory;
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
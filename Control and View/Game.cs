using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Model;

namespace My_game_for_Ulearn
{
    public class Game : UserControl
    {
        private readonly MainForm mainForm;
        private Map Map { get; set; }
        private Player Player { get; set; }
        private Timer Timer { get; }
        private Timer PathFinderTimer { get; }
        private Monster Monster { get; set; }
        private SinglyLinkedList<Point> Path { get; set; }
        
        public Game(MainForm form)
        {
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            mainForm = form;
            
            Player = new Player(Size.Width / 2, Size.Height / 2);
            Map = new Map(Size);

            Monster = new Monster(80, 80);
            
            Timer = new Timer { Interval = 10 };
            Timer.Start();
            Timer.Tick += OnTick;
            
            PathFinderTimer = new Timer { Interval = 1000 };
            PathFinderTimer.Start();
            PathFinderTimer.Tick += OnPathFinderTick;
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //TODO отрисовка тормозит, поправить
            var g = e.Graphics;
            
            var rect = new Rectangle(new Point(0, 0), Size);
            
            g.DrawImage(Map.Sprite, rect, Map.Anchor.X, Map.Anchor.Y, Size.Width, Size.Height, GraphicsUnit.Pixel);

            foreach (var item in Map.ItemsOnMap)
            {
                g.DrawImage(item.ItemSprite, rect, item.OnMapCoordinates.X, item.OnMapCoordinates.Y,
                    Size.Width, Size.Height, GraphicsUnit.Pixel);
            }

            g.DrawImage(Player.Sprite, Player.X - Player.Sprite.Width / 2, Player.Y - Player.Sprite.Height);
            
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\eIcon.png";
            var eIcon = (Bitmap)Image.FromFile(path);
            
            
            foreach (var itemCoords in Map.ItemsNearPlayer.Select(item => Map.GetOnMapCoordinates(item.X, item.Y - 40)))
            {
                g.DrawImage(eIcon, rect, itemCoords.X, itemCoords.Y,
                    Size.Width, Size.Height, GraphicsUnit.Pixel);
            }
            
            g.DrawImage(Monster.Sprite, rect, Map.Anchor.X - Monster.X, Map.Anchor.Y - Monster.Y + Monster.Sprite.Height,
                Size.Width, Size.Height, GraphicsUnit.Pixel);
        }
        
        private void OnTick(object sender, EventArgs e)
        {
            Path = Monster.Move(Path, Map.GridScale);
            Player.MovePlayer(Map);
            Invalidate(); 
        }
        
        private void OnPathFinderTick(object sender, EventArgs e)
        {
            var playerDistrict = new Point((int) (Map.Anchor.X + Player.X) / Map.GridScale,
                (int) (Map.Anchor.Y + Player.Y) / Map.GridScale);
            Path = PathFinder.FindPaths(Map.PathfinderGrid, 
                playerDistrict, 
                new Point(Monster.X / Map.GridScale, Monster.Y / Map.GridScale));
        }        
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            HandleKey(e.KeyCode, true);
            
            if (e.KeyCode == Keys.E)
            {
                if (Map.ItemsNearPlayer.Count(x => x.IsDialogable) != 0)
                    mainForm.StartDialog();
                
                if (Map.ItemsNearPlayer.Count(x => x.IsPickable) != 0)
                    Map.PickUpItem(Player.Inventory);
            }

            if (e.KeyCode == Keys.Escape)
                mainForm.PauseGame();

            if (e.KeyCode == Keys.F)
                mainForm.OpenInventory();
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
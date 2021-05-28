using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Model;

namespace My_game_for_Ulearn
{
    public class Game : UserControl
    {
        private readonly MainForm mainForm;
        private Map Map { get; }
        private Player Player { get; }
        private Timer Timer { get; set; }
        private Timer PathFinderTimer { get; set; }
        private Timer MonsterMoveTimer { get; set; }
        private Monster Monster { get; }
        
        public Game(MainForm form)
        {
            mainForm = form;
            ClientSize = mainForm.ClientSize;
            Map = new Map(Size);
            Player = new Player(Size.Width / 2, Size.Height / 2);
            
            Monster = new Monster(1000, 1000);

            InitializeTimers();
            Map.RedrawMap();
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void InitializeTimers()
        {
            Timer = new Timer { Interval = 10 };
            Timer.Start();
            Timer.Tick += OnTick;
            
            PathFinderTimer = new Timer { Interval = 1000 };
            PathFinderTimer.Start();
            PathFinderTimer.Tick += OnPathFinderTick;
            
            MonsterMoveTimer = new Timer { Interval = 20 };
            MonsterMoveTimer.Start();
            MonsterMoveTimer.Tick += OnMoveMonsterTick;
        }

        public void StopGame()
        {
            Timer.Stop();
            PathFinderTimer.Stop();
            MonsterMoveTimer.Stop();
        }
        
        public void StartGame()
        {
            Timer.Start();
            PathFinderTimer.Start();
            MonsterMoveTimer.Start();
        }

        private void DrawInterface()
        {
            
        }

        // private void RedrawMap()
        //         {
        //             var g = Graphics.FromImage(MapSprite);
        //             var rect = new Rectangle(new Point(0, 0), MapSprite.Size);
        //             
        //             g.DrawImage(Map.Sprite, rect, Map.Anchor.X, Map.Anchor.Y, Size.Width, Size.Height, GraphicsUnit.Pixel);
        //             
        //             foreach (var item in Map.ItemsOnMap)
        //             {
        //                 g.DrawImage(item.ItemSprite, rect, item.OnMapCoordinates.X, item.OnMapCoordinates.Y,
        //                     Size.Width, Size.Height, GraphicsUnit.Pixel);
        //             }
        //             
        //             var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\eIcon.png";
        //             var eIcon = (Bitmap)Image.FromFile(path);
        //             
        //             foreach (var itemCoords in Map.ItemsNearPlayer.Select(item => Map.GetOnMapCoordinates(item.X, item.Y - 40)))
        //             {
        //                 g.DrawImage(eIcon, rect, itemCoords.X, itemCoords.Y, 
        //                     Size.Width, Size.Height, GraphicsUnit.Pixel);
        //             }
        //         }
        
        protected override void OnPaint(PaintEventArgs e)
        {
             //TODO отрисовка тормозит, поправить
             var g = e.Graphics;
             DrawGame(g);
             DrawInterface(g);
        }

        private void DrawGame(Graphics g)
        {
            var rect = new Rectangle(new Point(0, 0), Size);
            g.DrawImage(Map.MapSprite, rect, Map.Anchor.X, Map.Anchor.Y, Size.Width, Size.Height, GraphicsUnit.Pixel);
            g.DrawImage(Player.Sprite, Player.X - Player.Sprite.Width / 2, Player.Y - Player.Sprite.Height);
            g.DrawImage(Monster.Sprite, rect, Map.Anchor.X - Monster.X, Map.Anchor.Y - Monster.Y + Monster.Sprite.Height, 
                Size.Width, Size.Height, GraphicsUnit.Pixel);
        }
        
        private void DrawInterface(Graphics g)
        {
            g.FillRectangle(Brushes.Black, Size.Width / 80, Size.Height / 40, Size.Width / 3, Size.Height / 10);
            g.FillRectangle(Brushes.Gray, Size.Width / 40, Size.Height / 20, Size.Width * 31 / 100, Size.Height / 20);
            g.FillRectangle(Brushes.RosyBrown, Size.Width / 40, Size.Height / 20,
                Player.MadnessScale.Value * Size.Width * 31 / (100 * Player.MadnessScale.maxValue) , Size.Height / 20);
        }
        
        private void OnTick(object sender, EventArgs e)
        {
            Player.MovePlayer(Map);

            var unhighlightedItems = Map.ItemsNearPlayer.Where(x => !x.IsHighlighted);
            if (unhighlightedItems.Count() != 0)
            {
                Map.RedrawMap();
                foreach (var item in unhighlightedItems)
                    item.IsHighlighted = true;
            }
            
            var highlightedItems = Map.ItemsOnMap.Where(x => x.IsHighlighted && !x.IsNearby);
            if (highlightedItems.Count() != 0)
            {
                Map.RedrawMap();
                foreach (var item in highlightedItems)
                    item.IsHighlighted = false;
            }
            
            if (Player.Damaging(Monster, Map.Anchor))
                Player.MadnessScale.Value++;
            else if(Player.MadnessScale.Value != Player.MadnessScale.minValue)
                Player.MadnessScale.Value--;

            if (Player.MadnessScale.Value >= Player.MadnessScale.maxValue) 
                mainForm.Die();
            Invalidate();
        }
        
        private void OnPathFinderTick(object sender, EventArgs e)
        {
            if (Player.NearbyMonsters(Monster, Map.Anchor))
            {
                var playerDistrict = new Point((int) (Map.Anchor.X + Player.X) / Map.GridScale,
                    (int) (Map.Anchor.Y + Player.Y) / Map.GridScale);
                var monsterDistrict = new Point(Monster.X / Map.GridScale, Monster.Y / Map.GridScale);
                Monster.Path = PathFinder.FindPaths(Map.PathfinderGrid, playerDistrict, monsterDistrict);
            }
        }   
        
        private void OnMoveMonsterTick(object sender, EventArgs e)
        {
            Monster.Path = Monster.Move(Monster.Path, Map.GridScale);
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            HandleKey(e.KeyCode, true);
            
            if (e.KeyCode == Keys.E)
            {
                if (Map.ItemsNearPlayer.Count(x => x.IsDialogable) != 0)
                    mainForm.StartDialog();
                
                if (Map.ItemsNearPlayer.Count(x => x.IsPickable) != 0)
                    Map.PickUpItem(Player);
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
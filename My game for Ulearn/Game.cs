using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Model;

namespace My_game_for_Ulearn
{
    public partial class MainForm : Form
    {
        public Map Map { get; set; }
        public Player Player { get; set; }
        public Timer moveTimer { get; }
        public Timer timer { get; }
        public List<OnMapItem> itemsOnMap { get; set; }
        public GameState GameState { get; set; }
        
        public MainForm()
        {
            InitializeComponent();
            GameState = GameState.Game;
            Player = new Player(Size.Width / 2, Size.Height / 2);
            Map = new Map();
            
            moveTimer = new Timer { Interval = 30 };
            moveTimer.Start();
            moveTimer.Tick += OnTickMove;
            
            timer = new Timer { Interval = 5 };
            timer.Start();
            timer.Tick += OnTick;
            
            Paint += OnPaintUpdate;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
            
            itemsOnMap = new List<OnMapItem>();
            itemsOnMap.Add(new OnMapItem("testItem", Size.Width / 2, Size.Height / 2));
        }
        
        private void OnTickMove(object sender, EventArgs e)
        {
            Player.Translate();
            Map.Anchor = new Point(Player.X - Size.Width / 2, Player.Y - Size.Height / 2);
        }
        
        private void OnTick(object sender, EventArgs e)
        {
            //Invalidate(); не понятно, чем отличается от Refresh
            Refresh(); 
        }
        
        private void OnPaintUpdate(object sender, PaintEventArgs e)
        {
            if (GameState == GameState.MainMenu)
            {
                Drawer.DrawMainMenu(sender, e);
            }
            if (GameState == GameState.Game)
            {
                Drawer.DrawGame(sender, e);
            }
            if (GameState == GameState.GameMenu)
            {
                Drawer.DrawGameMenu(sender, e);
            }
            if (GameState == GameState.Dialog)
            {
                Drawer.DrawDialog(sender, e);
            }
            if (GameState == GameState.Inventory)
            {
                Drawer.DrawInventory(sender, e);
            }
            if (GameState == GameState.SettingsMenu)
            {
                Drawer.DrawSettingsMenu(sender, e);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKey(e.KeyCode, true);
            
            if (e.KeyCode == Keys.E)
            {
                if (false)
                    GameState = GameState.Dialog;
                else
                    GameState = GameState.Inventory;
                var dialog = new Dialog("Это тестовоый диалог.");
                //DrawDialog(dialog, graphics, Size);
            }
        }
        
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            HandleKey(e.KeyCode, false);
        }
        
        private void HandleKey(Keys key, bool value)
        {
            if (key == Keys.A || key == Keys.Left) Player.WalkRight = value;
            if (key == Keys.D || key == Keys.Right) Player.WalkLeft = value;
            if (key == Keys.W || key == Keys.Up)Player.WalkBack = value;
            if (key == Keys.S || key == Keys.Down) Player.WalkForward = value;
        }
    }
}
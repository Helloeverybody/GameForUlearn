using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace My_game_for_Ulearn
{
    public partial class Form1 : Form
    {
        private Map Map;
        private Player Player;
        private readonly Timer moveTimer;
        private readonly Timer timer;
        private List<OnMapItem> itemsOnMap;
        public Form1()
        {
            InitializeComponent();
            
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
            var g = e.Graphics;
            var size = new Size(Size.Width * 4, Size.Height * 4);
            var rect = new Rectangle(Map.Anchor, size);
            g.DrawImage(Map.mapSprite, rect, 0, 0, Size.Width, Size.Height, GraphicsUnit.Point);
            g.DrawImage(Player.playerSprite, Size.Width / 2 - 25, Size.Height / 2 - 25, 50,50);
            
            foreach (var item in itemsOnMap)
                g.DrawImage(item.ItemSprite, rect, 0, 0, Size.Width, Size.Height, GraphicsUnit.Point);
            
            var nearbyItems = Player.CheckForItems(itemsOnMap);
            var itemSize = new Size(Size.Width, Size.Height);
            var itemRect = new Rectangle(Map.Anchor, size);
            foreach (var item in nearbyItems)
            {
                g.DrawImage(Player.playerSprite, rect, 0, 0, Size.Width, Size.Height, GraphicsUnit.Point);
            }
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKey(e.KeyCode, true);
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
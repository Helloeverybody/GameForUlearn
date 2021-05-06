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
using My_game_for_Ulearn.Model;

namespace My_game_for_Ulearn
{
    public partial class Form1 : Form
    {
        private Map Map;
        private Player Player;
        private readonly Timer timer;
        public Form1()
        {
            InitializeComponent();
            
            Player = new Player(Size.Width / 2, Size.Height / 2);
            Map = new Map(Size.Width, Size.Height);
            
            timer = new Timer {Interval = 10};
            timer.Start();
            timer.Tick += OnTick;
            
            Paint += OnPaintUpdate;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }
        
        private void OnTick(object sender, EventArgs e)
        {
            Player.Translate();
            //TODO 1. распараллелить RevaluateDistrict, 2. переделать условие пересчета RevaluateDistrict
            //Map.Anchor = new Point(0, 0);
            //Map.RevaluateDistrict(Size.Height, Size.Width); 
            
            //Invalidate(); не понятно, чем отличаются от Refresh
            Refresh(); 
        }
        
        private void OnPaintUpdate(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImage(Player.playerSprite, Player.X, Player.Y, 50,50);
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
            if (key == Keys.A || key == Keys.Left) Player.WalkLeft = value;
            if (key == Keys.D || key == Keys.Right) Player.WalkRight = value;
            if (key == Keys.W || key == Keys.Up)Player.WalkForward = value;
            if (key == Keys.S || key == Keys.Down) Player.WalkBackward = value;
        }
    }
}
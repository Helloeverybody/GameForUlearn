using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using My_game_for_Ulearn.Model;

namespace My_game_for_Ulearn
{
    public partial class Form1 : Form
    {
        Player Player = new Player(0, 0);
        private Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 16;
            timer.Start();
            timer.Tick += OnTick;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            
        }
        
        private void OnTick(object sender, EventArgs e)
        {
            Player.Translate();
            Invalidate();
            //Refresh(); не понятно, чем отличаются
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImage(Player.playerSprite, Player.X, Player.Y, 50,50);
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKey(e.KeyCode);
        }
        
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            HandleKey(e.KeyCode);
        }
        
        private void HandleKey(Keys key)
        {
            if (key == Keys.A || key == Keys.Left) Player.WalkLeft = true;
            if (key == Keys.D || key == Keys.Right) Player.WalkRight = true;
            if (key == Keys.W || key == Keys.Up)Player.WalkForward = true;
            if (key == Keys.S || key == Keys.Down) Player.WalkBackward = true;
        }
    }
}
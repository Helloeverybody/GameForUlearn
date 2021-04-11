using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_game_for_Ulearn
{
    public partial class Form1 : Form
    {
        Player Player = new Player(0, 0);
        public Form1()
        {
            InitializeComponent();
            KeyPress += new KeyEventHandler(MainForm_KeyPressed(this,));
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImage(Player.playerSprite, Player.X, Player.Y);
        }
        
        void MainForm_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w')
            {
                Player.Y += 1;
                Console.WriteLine($"{Player.X}");
            }
            if (e.KeyChar == 'a')
            {
                Player.X -= 1;
            }
            if (e.KeyChar == 's')
            {
                Player.Y -= 1;
            }
            if (e.KeyChar == 'd')
            {
                Player.X += 1;
            }
        }
    }
}
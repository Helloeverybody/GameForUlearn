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
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImageUnscaled(Player.playerSprite, 100, -100);
        }
    }
}
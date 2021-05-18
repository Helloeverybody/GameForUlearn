using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace My_game_for_Ulearn
{
    public class MainMenu : UserControl
    {
        private MainForm mainForm;
        private IContainer components;
        
        public MainMenu(MainForm form)
        {
            mainForm = form;
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            var playButton = new Button
            {
                Text = @"Play",
                Location = new Point(mainForm.Size.Width / 10, mainForm.Size.Height * 7 / 10),
                Size = new Size(mainForm.Size.Width / 5, mainForm.Size.Height / 10)
            };

            playButton.Click += OnPlayButtonClick;
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            components = new Container();
            AutoScaleMode = AutoScaleMode.Font;
        }

        private void OnPlayButtonClick(object sender, EventArgs e)
        {
            mainForm.MainMenu.Enabled = false;
            mainForm.MainMenu.Hide();
            mainForm.Game.Enabled = true;
            mainForm.Game.Show();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.DarkSlateGray);
            g.FillRectangle(Brushes.Black, Size.Width / 10, Size.Height * 2 / 3,
                Size.Width * 4 / 5, Size.Height / 5);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
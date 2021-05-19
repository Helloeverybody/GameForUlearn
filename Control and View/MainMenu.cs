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
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            var playButton = new Button
            {
                Text = @"Играть",
                Location = new Point(Size.Width / 3, Size.Height * 7 / 16),
                Size = new Size(Size.Width / 3, Size.Height / 8)
            };

            playButton.Click += OnPlayButtonClick;
            Controls.Add(playButton);
        }

        private void OnPlayButtonClick(object sender, EventArgs e)
        {
            mainForm.StartGame();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.DarkSlateGray);
            g.FillRectangle(Brushes.Black, Size.Width / 3, Size.Height / 10,
                Size.Width / 3, Size.Height / 5);
            var font = new Font("SlimamifMedium", 60, FontStyle.Bold, GraphicsUnit.Pixel);
            g.DrawString("МОЯ ИГРА", font, Brushes.White, new PointF(Size.Width * 4 / 10, 
                Size.Height / 10 + 20), StringFormat.GenericTypographic);
        }
    }
}
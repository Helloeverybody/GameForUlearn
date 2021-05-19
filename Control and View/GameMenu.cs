using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace My_game_for_Ulearn
{
    public class GameMenu : UserControl
    {
        private MainForm mainForm;
        private IContainer components;
        
        public GameMenu(MainForm form)
        {
            mainForm = form;
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            var continueButton = new Button
            {
                Text = @"Продолжить",
                Location = new Point(Size.Width / 3, Size.Height * 7 / 16),
                Size = new Size(Size.Width / 3, Size.Height / 8)
            };
            continueButton.Click += OnContinueButtonClick;
            Controls.Add(continueButton);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var font = new Font("SlimamifMedium", 80, FontStyle.Bold, GraphicsUnit.Pixel);
            g.DrawString("Меню", font, Brushes.Black, new PointF(Size.Width / 9, 
                Size.Height * 2 / 3), StringFormat.GenericTypographic);
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            //почему-то абсолютно не работает....
            if (e.KeyCode == Keys.Escape)
            {
                mainForm.ContinueGame();
            }
        }
        
        private void OnContinueButtonClick(object sender, EventArgs e)
        {
            mainForm.ContinueGame();
        }
    }
}
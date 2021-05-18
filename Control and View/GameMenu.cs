using System;
using System.ComponentModel;
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
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            var continueButton = new Button();
            continueButton.Click += OnContinueButtonClick;
            
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            components = new Container();
            AutoScaleMode = AutoScaleMode.Font;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
        }
        
        private void OnContinueButtonClick(object sender, EventArgs e)
        {
            
        }
    }
}
using System.Drawing;
using System.Windows.Forms;

namespace My_game_for_Ulearn
{
    public class MainForm : Form
    {

        public MainMenu MainMenu;
        public Game Game;
        public Inventory Inventory;
        public GameMenu GameMenu;
        public Dialog Dialog;
        
        public MainForm()
        {
            MainMenu = new MainMenu(this);
            Game = new Game(this) {Enabled = true};
            Game.Show();
            Inventory = new Inventory(this);
            GameMenu = new GameMenu(this);
            Dialog = new Dialog(this);

            SuspendLayout();
            Text = "Game";
            WindowState = FormWindowState.Maximized;
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            Controls.AddRange(new Control[] {MainMenu, Game, Inventory, GameMenu, Dialog});
            ResumeLayout(false);
            
            
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }
    }
}
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
            MainMenu.Hide();
            Game = new Game(this) {Enabled = true};
            Game.Show();
            Inventory = new Inventory(this);
            Inventory.Hide();
            GameMenu = new GameMenu(this);
            GameMenu.Hide();
            Dialog = new Dialog(this);
            Dialog.Hide();
            
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
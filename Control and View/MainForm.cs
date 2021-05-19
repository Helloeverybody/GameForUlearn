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
            MainMenu = new MainMenu(this) {Enabled = true};
            MainMenu.Show();
            
            Game = new Game(this) {Enabled = false};
            Game.Hide();
            
            Inventory = new Inventory(this) {Enabled = false};
            Inventory.Hide();
            
            GameMenu = new GameMenu(this) {Enabled = false};
            GameMenu.Hide();
            
            Dialog = new Dialog(this) {Enabled = false};
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

        public void StartGame()
        {
            FromControlToControl(MainMenu, Game);
        }
        
        public void PauseGame()
        {
            FromControlToControl(Game, GameMenu);
        }
        
        public void OpenInventory()
        {
            FromControlToControl(Game, Inventory);
        }
        
        public void CloseInventory()
        {
            FromControlToControl(Inventory, Game);
        }
        
        public void ContinueGame()
        {
            FromControlToControl(GameMenu, Game);
        }
        
        public void StartDialog()
        {
            FromControlToControl(Game, Dialog);
        }
        public void EndDialog()
        {
            FromControlToControl(Dialog, Game);
        }

        public void FromControlToControl(UserControl fromThis, UserControl toThis)
        {
            fromThis.Enabled = false;
            fromThis.Hide();
            toThis.Enabled = true;
            toThis.Show();
            toThis.Focus();
        }
    }
}
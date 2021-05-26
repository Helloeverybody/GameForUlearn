using System.Drawing;
using System.Windows.Forms;

namespace My_game_for_Ulearn
{
    public class MainForm : Form
    {
        private MainMenu mainMenu;
        private Game game;
        private Inventory inventory;
        private GameMenu gameMenu;
        private Dialog dialog;
        
        public MainForm()
        {
            mainMenu = new MainMenu(this) { Enabled = true };
            mainMenu.Show();
            
            game = new Game(this) { Enabled = false };
            game.Hide();
            
            inventory = new Inventory(this) { Enabled = false };
            inventory.Hide();
            
            gameMenu = new GameMenu(this) { Enabled = false };
            gameMenu.Hide();
            
            dialog = new Dialog(this) { Enabled = false };
            dialog.Hide();
            
            SuspendLayout();
            Text = "Game";
            WindowState = FormWindowState.Maximized;
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            Controls.AddRange(new Control[] {mainMenu, game, inventory, gameMenu, dialog});
            ResumeLayout(false);
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        public void StartGame()
        {
            FromControlToControl(mainMenu, game);
        }
        
        public void PauseGame()
        {
            game.StopGame();
            FromControlToControl(game, gameMenu);
        }
        
        public void ContinueGame()
        {
            game.StartGame();
            FromControlToControl(gameMenu, game);
        }
        
        public void OpenInventory()
        {
            FromControlToControl(game, inventory);
        }
        
        public void CloseInventory()
        {
            FromControlToControl(inventory, game);
        }
        
        public void ExitToMainMenu()
        {
            FromControlToControl(gameMenu, mainMenu);
        }
        
        public void StartDialog()
        {
            FromControlToControl(game, dialog);
        }
        
        public void EndDialog()
        {
            FromControlToControl(dialog, game);
        }

        private static void FromControlToControl(UserControl fromThis, UserControl toThis)
        {
            fromThis.Enabled = false;
            fromThis.Hide();
            toThis.Enabled = true;
            toThis.Show();
            toThis.Focus();
        }
    }
}
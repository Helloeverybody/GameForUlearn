using System.Drawing;
using System.Windows.Forms;
using Model;

namespace My_game_for_Ulearn
{
    public class MainForm : Form
    {
        private Game game;
        private Dead dead;
        private Dialog dialog;
        private GameMenu gameMenu;
        private MainMenu mainMenu;
        public Inventory inventory;
        
        public MainForm()
        {
            mainMenu = new MainMenu(this) { Enabled = true };
            mainMenu.Show();
            
            gameMenu = new GameMenu(this) { Enabled = false };
            gameMenu.Hide();
            
            dialog = new Dialog(this) { Enabled = false };
            dialog.Hide();
            
            dead = new Dead(this) { Enabled = false };
            dead.Hide();
            
            SuspendLayout();
            Text = "Game";
            WindowState = FormWindowState.Maximized;
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            Controls.AddRange(new Control[] {mainMenu, game, inventory, gameMenu, dialog, dead});
            ResumeLayout(false);
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        

        public void StartGame()
        {
            //костыль, игра должна начинаться с начала
            game = new Game(this) { Enabled = false };
            game.Hide();
            Controls.Add(game);
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
            inventory = new Inventory(this, game.Player.Inventory);
            Controls.Add(inventory);
            game.StopGame();
            FromControlToControl(game, inventory);
        }
        
        public void CloseInventory()
        {
            game.StartGame();
            FromControlToControl(inventory, game);
        }
        
        public void ExitToMainMenu()
        {
            FromControlToControl(gameMenu, mainMenu);
        }
        
        public void StartDialog()
        {
            game.StopGame();
            FromControlToControl(game, dialog);
        }
        
        public void EndDialog()
        {
            game.StartGame();
            FromControlToControl(dialog, game);
        }
        
        public void FromDeadToMenu()
        {
            FromControlToControl(dead, mainMenu);
        }
        
        public void Die()
        {
            FromControlToControl(game, dead);
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
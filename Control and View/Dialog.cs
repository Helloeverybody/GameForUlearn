using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace My_game_for_Ulearn
{
    public class Dialog : UserControl
    {
        private static MainForm mainForm;
        private IContainer components;
        public Bitmap Background = new Bitmap(1, 1);
        
        public Dialog(MainForm form)
        {
            //TODO возможно стоит вообще убрать диалоги из юзерконтролс
            mainForm = form;
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImage(Background, new Point(0, 0));
            //DrawGame(sender, e); надо бы за диалогом игру отрисовывать....
            
            var a = new Model.Dialog("Это тестовоый диалог. Это тестовоый диалог. Это тестовоый диалог. Это тестовоый диалог.");
            a.DrawDialog(g, mainForm.Size);
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E)
            {
                mainForm.EndDialog();
            }
        }
    }
}
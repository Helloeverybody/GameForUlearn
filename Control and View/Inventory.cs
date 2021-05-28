using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace My_game_for_Ulearn
{
    public class Inventory : UserControl
    {
        private MainForm mainForm;
        
        public Inventory(MainForm form)
        {
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            mainForm = form;
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.F)
            {
                mainForm.CloseInventory();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            
            var font = new Font("SlimamifMedium", 80, FontStyle.Bold, GraphicsUnit.Pixel);
            g.DrawString("Инвентарь", font, Brushes.Black, new PointF(Size.Width / 9, 
                Size.Height * 2 / 3), StringFormat.GenericTypographic);
        }
    }
}
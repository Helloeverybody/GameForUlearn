using System.Drawing;
using System.Windows.Forms;

namespace My_game_for_Ulearn
{
    public class Dead : UserControl
    {
        private readonly MainForm mainForm;
        public Dead(MainForm mainForm)
        {
            this.mainForm = mainForm;
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.FillRectangle(Brushes.Salmon, 0, 0, mainForm.Size.Width, mainForm.Size.Height);
            var font = new Font("SlimamifMedium", 270, FontStyle.Bold, GraphicsUnit.Pixel);
            g.DrawString("КОНЕЦ ИГРЫ", font, Brushes.White, new PointF(mainForm.Width / 100, 
                mainForm.Height / 5), StringFormat.GenericTypographic);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            mainForm.FromDeadToMenu();
        }
    }
}
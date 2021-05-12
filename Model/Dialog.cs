using System;
using System.Drawing;

namespace Model
{
    public class Dialog
    {
        public string Text;
        public Image Background;

        public Dialog(string text)
        { 
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Assets\DialogBackground.png";
            Background = Image.FromFile(path);
            Text = text;
        }

        public void DrawDialog(Graphics g, Size windowSize)
        {
            var font = new Font("SlimamifMedium", 40, FontStyle.Bold, GraphicsUnit.Pixel);
            var pen = new Pen(Color.Black);
            g.FillRectangle(Brushes.Black, windowSize.Width / 10, windowSize.Height * 2 / 3,
                windowSize.Width * 4 / 5, windowSize.Height / 5);
            // декоративные прямоугольники
            //g.DrawRectangle(Pens.Tan, , ,windowSize.Width - 50, windowSize.Height / 6);
            //g.DrawRectangle(Pens.Black, , , windowSize.Width - 60, windowSize.Height / 6);
            //var splittedText 
            g.DrawString(Text, font, Brushes.White, new PointF(windowSize.Width / 9, 
                windowSize.Height * 2 / 3), StringFormat.GenericTypographic); 
        }
    }
}
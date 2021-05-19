using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
            g.FillRectangle(Brushes.Black, windowSize.Width / 10, windowSize.Height * 2 / 3,
                windowSize.Width * 4 / 5, windowSize.Height / 5);
            //g.DrawImage();
            
            // декоративные прямоугольники (нгдо бы заменить спрайтом)
            //g.FillRectangle(Brushes.Tan, windowSize.Width * 31 / 300, windowSize.Height * 2 / 3 + 10,
            //    windowSize.Width * 237 / 300, windowSize.Height / 5 - 20);
            //g.DrawRectangle(Pens.Black, , , windowSize.Width - 60, windowSize.Height / 6);
            //var splittedText 
            
            // можно по таймеру отсчитывать время, через которое будет отрисовываться один символ
            // делать это придется, очевидно, через многопоточку

            var timer = new Timer();
            timer.Interval = 100;
            timer.Start();
            
            
            
            var letters = Text.ToArray();
            var font = new Font("SlimamifMedium", 20, FontStyle.Bold, GraphicsUnit.Pixel);
            float lineWidth = 0;
            float lineHeight = 0;
            foreach (var letter in letters)
            {
                lineWidth += font.Size / 2;
                if (lineWidth >= windowSize.Width * 7 / 10)
                {
                    lineHeight += font.Size;
                    lineWidth = font.Size / 2;
                }
                    
                g.DrawString(letter.ToString(), font, Brushes.White, new PointF(windowSize.Width / 9 + lineWidth, 
                    windowSize.Height * 2 / 3 + lineHeight), StringFormat.GenericTypographic);
            }
        }

        public IEnumerable<string> GetLetter()
        {
            var letters = Text.ToArray();
            foreach (var letter in letters)
                yield return letter.ToString();
        }
    }
}
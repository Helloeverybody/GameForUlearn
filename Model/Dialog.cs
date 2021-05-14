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
            var font = new Font("SlimamifMedium", 40, FontStyle.Bold, GraphicsUnit.Pixel);
            g.FillRectangle(Brushes.Black, windowSize.Width / 10, windowSize.Height * 2 / 3,
                windowSize.Width * 4 / 5, windowSize.Height / 5);
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
            float fontWidth = 0;
            foreach (var letter in letters)
            {
                fontWidth += font.Size / 2;
                g.DrawString(letter.ToString(), font, Brushes.White, new PointF(windowSize.Width / 9 + fontWidth, 
                    windowSize.Height * 2 / 3), StringFormat.GenericTypographic); 
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
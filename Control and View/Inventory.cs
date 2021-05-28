using System;
using System.Drawing;
using System.Windows.Forms;
using Model;

namespace My_game_for_Ulearn
{
    public class Inventory : UserControl
    {
        private MainForm mainForm;
        private Model.Inventory inventory;
        private Bitmap CellSprite;
        
        public Inventory(MainForm form, Model.Inventory inventory)
        {
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
            mainForm = form;
            this.inventory = inventory;
            
            var cellPath = AppDomain.CurrentDomain.BaseDirectory + @"Assets\HUD\Bars\Others\Horizontal\Switch_1.png";
            CellSprite = (Bitmap)Image.FromFile(cellPath);
            
            BackColor = Color.Sienna;
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

            var widthScale = Width / 100;
            var heightScale = Width / 100;

            var cellNumber = 0;
            for (var h = heightScale * 10; h < heightScale * 50; h += heightScale * 8)
            {
                for (var w = widthScale * 5; w < widthScale * 40; w += widthScale * 8)
                {
                    var cellRectangle = new Rectangle(
                        new Point(w - CellSprite.Width / 2, h - CellSprite.Height / 2),
                        new Size(widthScale * 8, heightScale * 8));
                    g.DrawImage(CellSprite, cellRectangle, 0, 0, widthScale * 4, heightScale * 4, GraphicsUnit.Pixel);


                    if (cellNumber < inventory.Count)
                    {
                        var item = inventory[cellNumber++];
                        var itemRectangle = new Rectangle(
                            new Point(w - item.Sprite.Width / 4, h - item.Sprite.Height / 4),
                            new Size(widthScale * 8, heightScale * 8));
                        g.DrawImage(item.Sprite, itemRectangle, 0, 0, widthScale * 8, heightScale * 8, GraphicsUnit.Pixel);
                    } 
                }
            }
            
            var font = new Font("SlimamifMedium", 60, FontStyle.Bold, GraphicsUnit.Pixel);
            g.DrawString("Инвентарь", font, Brushes.Black, new PointF(Size.Width / 50, 
                Size.Height / 50), StringFormat.GenericTypographic);
        }
    }
}
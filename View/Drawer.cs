using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Model;

namespace View
{
    public class Drawer
    {
        public static void DrawMainMenu(object sender, PaintEventArgs e)
        {
            var form = (Form) sender;
            var g = e.Graphics;
            var size = new Size(form.Size.Width * 4, form.Size.Height * 4);
            var rect = new Rectangle(form.Map.Anchor, size);
            g.DrawImage(form.Map.mapSprite, rect, 0, 0, Size.Width, Size.Height, GraphicsUnit.Point);
            g.DrawImage(form.Player.playerSprite, Size.Width / 2 - 25, Size.Height / 2 - 25, 50,50);
            
            foreach (var item in form.itemsOnMap)
                g.DrawImage(item.ItemSprite, rect, 0, 0, Size.Width, Size.Height, GraphicsUnit.Point);
            
            var nearbyItems = Player.CheckForItems(itemsOnMap);
            foreach (var item in nearbyItems)
            {
                g.DrawImage(Player.playerSprite, rect, 0, 0, Size.Width, Size.Height, GraphicsUnit.Point);
            }
            
            var a = new VisualStyleElement.Window.Dialog("Это тестовоый диалог.");
            a.DrawDialog(g, Size);
        }
        
        public static void DrawGame(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
        }
        
        public static void DrawGameMenu(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
        }
        
        public static void DrawDialog(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
        }
        
        public static void DrawInventory(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
        }
        
        public static void DrawSettingsMenu(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
        }
    }
}
using System.Drawing;
using System.Windows.Forms;
using Model;

namespace My_game_for_Ulearn
{
    public static class Drawer
    {
        public static void DrawMainMenu(object sender, PaintEventArgs e)
        {
            var form = (MainForm) sender;
            var g = e.Graphics;
            g.Clear(Color.DarkSlateGray);
            g.FillRectangle(Brushes.Black, form.Size.Width / 10, form.Size.Height * 2 / 3,
                form.Size.Width * 4 / 5, form.Size.Height / 5);
        }
        
        public static void DrawGame(object sender, PaintEventArgs e)
        {
            var form = (MainForm) sender;
            var g = e.Graphics;
            var size = new Size(form.Size.Width * 4, form.Size.Height * 4);
            var rect = new Rectangle(form.Map.Anchor, size);
            g.DrawImage(form.Map.mapSprite, rect, 0, 0, form.Size.Width, form.Size.Height, GraphicsUnit.Point);
            g.DrawImage(form.Player.playerSprite, form.Size.Width / 2 - 25, form.Size.Height / 2 - 25, 50,50);
            
            foreach (var item in form.itemsOnMap)
                g.DrawImage(item.ItemSprite, rect, 0, 0, form.Size.Width, form.Size.Height, GraphicsUnit.Point);
            
            var nearbyItems = form.Player.NearbyItems(form.itemsOnMap);
            foreach (var item in nearbyItems)
                g.DrawImage(form.Player.playerSprite, rect, 0, 0, form.Size.Width, form.Size.Height, GraphicsUnit.Point);
        }
        
        public static void DrawGameMenu(object sender, PaintEventArgs e)
        {
            var form = (MainForm) sender;
            var g = e.Graphics;
        }
        
        public static void DrawDialog(object sender, PaintEventArgs e)
        {
            var form = (MainForm) sender;
            var g = e.Graphics;
            DrawGame(sender, e);
            var a = new Dialog("Это тестовоый диалог.");
            a.DrawDialog(g, form.Size);
        }
        
        public static void DrawInventory(object sender, PaintEventArgs e)
        {
            var form = (MainForm) sender;
            var g = e.Graphics;
        }
        
        public static void DrawSettingsMenu(object sender, PaintEventArgs e)
        {
            var form = (MainForm) sender;
            var g = e.Graphics;
        }
    }
}
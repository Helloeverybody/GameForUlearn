using System.Drawing;

namespace My_game_for_Ulearn.Model
{
    public class Item
    {
        public string Name;
        public int Weight;
        public Image Icon;

        public Item(string name)
        {
            Name = name;
        }
    }
}
using System.Drawing;

namespace Model
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
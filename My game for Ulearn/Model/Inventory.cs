using System.Collections.Generic;

namespace My_game_for_Ulearn.Model
{
    public class Inventory
    {
        private List<Item> inventory = new List<Item>();

        public void Add(Item item)
        {
            inventory.Add(item);
        }

        public void Delete(Item item)
        {
            inventory.Remove(item);
        }
        
        public void Clear()
        {
            inventory = new List<Item>();
        }
    }
}
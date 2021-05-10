using System.Collections.Generic;

namespace Model
{
    public class Inventory
    {
        private List<OnMapItem> inventory = new List<OnMapItem>();

        public void Add(OnMapItem onMapItem)
        {
            inventory.Add(onMapItem);
        }

        public void Delete(OnMapItem onMapItem)
        {
            inventory.Remove(onMapItem);
        }
        
        public void Clear()
        {
            inventory = new List<OnMapItem>();
        }
    }
}
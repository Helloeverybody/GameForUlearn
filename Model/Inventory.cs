using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Inventory
    {
        private List<InventoryItem> inventory = new List<InventoryItem>();

        public int InventoryWeight
        {
            get
            {
                var weight = 0;
                inventory.Select(x => weight += x.Weight);
                return weight;
            }
        }

        public void Add(InventoryItem inventoryItem)
        {
            inventory.Add(inventoryItem);
        }

        public void Delete(InventoryItem inventoryItem)
        {
            inventory.Remove(inventoryItem);
        }
        
        public void Clear()
        {
            inventory = new List<InventoryItem>();
        }
    }
}
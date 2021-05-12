using System.Collections.Generic;

namespace Model
{
    public class Inventory
    {
        private List<InventoryItem> inventory = new List<InventoryItem>();

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
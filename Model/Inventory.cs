using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Inventory
    {
        private Dictionary<int, InventoryItem> inventory = new Dictionary<int, InventoryItem>();

        public int Count => inventory.Count;

        public int Capacity = 25;

        public int InventoryWeight
        {
            get
            {
                var weight = 0;
                inventory.Select(x => weight += x.Value.Weight);
                return weight;
            }
        }

        public InventoryItem this[int index]
        {
            get { return inventory[index]; }
        }

        public bool Add(OnMapItem onMapItem)
        {
            if (inventory.Count == Capacity)
                return false;
            inventory[inventory.Count] = new InventoryItem
            {
                Weight = onMapItem.Weight,
                Sprite = onMapItem.Sprite,
                Name = onMapItem.Name
            };
            return true;
        }

        public void Drop(int cellNumber)
        {
            //inventoryItem.OnMap();
            inventory[cellNumber] = new InventoryItem();
        }
        
        public void Clear()
        {
            inventory = new Dictionary<int, InventoryItem>();
        }
    }
}
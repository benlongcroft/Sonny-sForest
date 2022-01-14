using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        private Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
        public List<InventoryItem> Inventory { get; private set; }

        public List<InventoryItemData> startSeeds;

        private void Awake()
        {
            Inventory = new List<InventoryItem>();
            _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
            foreach (var seed in startSeeds)
            {
                this.Add(seed);
            }
        }

        public int Find(string itemName)
        {
            int i = 0;
            foreach (var item in Inventory)
            {
                if (item.Data.displayName == itemName)
                {
                    return i;
                }

                i++;
            }

            return -1;
        }
        

        public void Add(InventoryItemData referenceData)
        {
            if (_itemDictionary.TryGetValue(referenceData, out var value))
            {
                value.AddToStack();
            }
            else
            {
                var newItem = new InventoryItem(referenceData);
                Inventory.Add(newItem);
                _itemDictionary.Add(referenceData, newItem);
            }
        }

        public void Remove(InventoryItemData referenceData)
        {
            if (!_itemDictionary.TryGetValue(referenceData, out var value)) return;
            value.RemoveFromStack();
            if (value.StackSize == 0)
            {
                Inventory.Remove(value);
                _itemDictionary.Remove(referenceData);
            }
        }
    }
}
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
                if (seed.displayName == "Money")
                {
                    for (int x = 0; x<200; x++)
                    {
                        this.Add(seed);
                    }
                }
                else
                {
                    for (var i = 0; i < 200; i++)
                    {
                        this.Add(seed);
                    }
                }
            }
        }

        public int Find(string name)
        {
            int i = 0;
            foreach (var item in Inventory)
            {
                if (item.Data.displayName == name)
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
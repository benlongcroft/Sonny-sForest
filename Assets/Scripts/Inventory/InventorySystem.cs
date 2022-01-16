using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        private Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
        public List<InventoryItem> Inventory { get; private set; }

        public List<InventoryItemData> startSeeds;
        
        private void AddN(int n, InventoryItemData x)
        {
            for (int i = 0; i < n; i++)
            {
                this.Add(x);
            }
        }

        
        private void Awake()
        {
            Inventory = new List<InventoryItem>();
            _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
            foreach (var item in startSeeds)
            {
                switch (item.displayName)
                {
                    case "Money":
                        AddN(10, item);
                        break;
                    case "Ackerieva Apple":
                        AddN(2, item);
                        break;
                    case "Fovir Fir":
                        AddN(5, item);
                        break;
                    case "Golucki Gladious":
                        AddN(1, item);
                        break;
                    case "Nurgi Needle":
                        AddN(5, item);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(item.displayName));
                }
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
            // if (value.StackSize == 0)
            // {
            //     Inventory.Remove(value);
            //     _itemDictionary.Remove(referenceData);
            // }
        }
    }
}
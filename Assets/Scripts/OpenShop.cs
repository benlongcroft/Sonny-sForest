using System;
using System.Collections.Generic;
using System.Linq;
using Forest;
using Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Main
{
    public class OpenShop : MonoBehaviour
    {
        public GameObject panel;
        public InventorySystem myInventory;
        public Field[] myForest;

        private Dictionary<int, int> costs = new Dictionary<int, int>()
            {{0, 50}, {1, 100}, {2, 200}, {3, 300}, {4, 500}, {5, 600}, {6, 700}, {7, 800}};
        private float balance = 0;
        public Text balanceText;
        public Text fieldCost;

        public void GameView() {  
            panel.SetActive(false);
        }  
        public void Shopview() {  
            panel.SetActive(true);

        }

        private int GETFieldCount()
        {
            return myForest.Count(field => field.unlocked);
        }

        private int GETMoneyStack()
        {
            return myInventory.Inventory[0].StackSize;
        }

        private void Awake()
        {
            balanceText.text = (GETMoneyStack()).ToString();
            balance = GETMoneyStack();

            int fields = GETFieldCount();
            fieldCost.text = costs[fields].ToString();
        }

        public void BuyField()
        {
            int nFields = GETFieldCount()-1;
            balance = myInventory.Inventory[0].StackSize;
            if (balance >= costs[nFields])
            {
                for (int i = 0; i < costs[nFields]; i++)
                {
                    myInventory.Inventory[0].RemoveFromStack();
                }
                balanceText.text = (balance - costs[nFields]).ToString();
                balance -= costs[nFields];
                myForest[nFields+1].SetActive();
                fieldCost.text = costs[GETFieldCount()-1].ToString();
            }
        }

        public void Convert()
        {
            
        }
    }
}

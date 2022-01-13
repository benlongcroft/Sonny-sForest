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


        private float balance = 0;
        public Text balanceText;

        public void GameView() {  
            panel.SetActive(false);
        }  
        public void Shopview() {  
            panel.SetActive(true);

        }

        void Awake()
        {
            SetBalance(myInventory.Inventory[0].StackSize);
        }

        private void SetBalance(float amount)
        {
            balanceText.text = (balance + amount).ToString();
        }

        public void BuyField()
        {
            var costs = new Dictionary<int, int>()
                {{0, 50}, {1, 100}, {2, 200}, {3, 300}, {4, 500}, {5, 600}, {6, 700}, {7, 800}};
            var nFields = myForest.Count(field => field.unlocked)-1;
            balance = myInventory.Inventory[0].StackSize;
            if (balance >= costs[nFields])
            {
                for (int i = 0; i < costs[nFields]; i++)
                {
                    myInventory.Inventory[0].RemoveFromStack();
                }
                SetBalance(-nFields);
                balance -= costs[nFields];
                myForest[nFields].SetActive();
            }
        }

        public void Convert()
        {
            
        }
    }
}

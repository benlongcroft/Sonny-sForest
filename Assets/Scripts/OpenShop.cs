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

        private readonly Dictionary<int, int> FieldCosts = new Dictionary<int, int>()
            {{0, 50}, {1, 100}, {2, 200}, {3, 300}, {4, 500}, {5, 600}, {6, 700}, {7, 800}};

        private int balance = 0;
        public Text balanceText;
        public Text fieldCost;
        public Text conversionValue;
        public InputField quantity;

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

        private void ChangeBalance(float amount)
        {
            balanceText.text = (balance + amount).ToString();
            balance = (int) (balance + amount);
            LoadOut.Instance.SetBalance(balance);
            if (amount < 0)
            {
                for (var _ = amount; _ < 0; _++)
                {
                    myInventory.Inventory[0].RemoveFromStack();
                }
            }
            else
            {
                for (var _ = 0; _ < amount; _++)
                {
                    myInventory.Inventory[0].AddToStack();
                }
            }
            // CharController.SetLoadOut(myInventory.Inventory[0]);
        }

        private void Awake()
        {
            var amount = GETMoneyStack();
            balanceText.text = (balance + amount).ToString();
            balance = balance + amount;
            LoadOut.Instance.SetBalance(balance);

            var fields = GETFieldCount();
            fieldCost.text = FieldCosts[fields].ToString();
        }

        private void Update()
        {
            balance = myInventory.Inventory[0].StackSize;
            balanceText.text = balance.ToString();
        }

        public void BuyField()
        {
            var nFields = GETFieldCount()-1;
            balance = myInventory.Inventory[0].StackSize;
            if (balance >= FieldCosts[nFields])
            {
                ChangeBalance(-1*FieldCosts[nFields]);
                myForest[nFields+1].SetActive();
                fieldCost.text = FieldCosts[GETFieldCount()-1].ToString();
            }
        }

        private int GETLoadOutIndex()
        {
            var itemName = LoadOut.Instance.itemLabel.text;
            
            var index = myInventory.Find(itemName);
            return index;
        }

        private int CheckQuantity()
        {
            var qInput = quantity.text;
            var isNumeric = int.TryParse(qInput, out int amount);
            if (isNumeric)
            {
                return amount;
            }

            return -1;
        }

        private void Value()
        {
            float rate = 0;
            var index = GETLoadOutIndex();
            
            var id = int.Parse(myInventory.Inventory[index].Data.id);
            var maxQuantity = myInventory.Inventory[index].StackSize;
            var exchangeRate = myInventory.Inventory[index].Data.exchangeValue;
            
            var amount = CheckQuantity();

            if (amount <= 0 || amount >= maxQuantity) return;
            
            rate = (exchangeRate * amount);
            conversionValue.text = rate.ToString();


        }

        public void Convert()
        {
            var index = GETLoadOutIndex();
            if (index == 0) return;
            var id = int.Parse(myInventory.Inventory[index].Data.id);
            var maxQuantity = myInventory.Inventory[index].StackSize;
            var exchangeRate = myInventory.Inventory[index].Data.exchangeValue;
            
            var amount = CheckQuantity();
            
            if (amount <= 0 || amount >= maxQuantity) return;
            
            var coinsGained = (int) (exchangeRate * amount);
            conversionValue.text = coinsGained.ToString();

            for (var x = 0; x < amount; x++)
            {
                myInventory.Inventory[index].RemoveFromStack();
            }

            ChangeBalance(coinsGained);
        }
    }
}

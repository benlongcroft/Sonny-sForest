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
    /*
     * Shop Controller class
     */
    public class OpenShop : MonoBehaviour
    {
        public GameObject panel;
        public InventorySystem myInventory;
        public Field[] myForest;

        private readonly Dictionary<int, int> m_FieldCosts = new Dictionary<int, int>()
            {{0, 50}, {1, 100}, {2, 200}, {3, 300}, {4, 500}, {5, 600}, {6, 700}, {7, 800}};

        private int m_Balance = 0;
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
            return myForest.Count(field => field.Unlocked);
        }

        private int GETMoneyStack()
        {
            return myInventory.Inventory[0].StackSize;
        }

        private void ChangeBalance(float amount)
        {
            // Change balance of Rune Coins
            balanceText.text = (m_Balance + amount).ToString();
            m_Balance = (int) (m_Balance + amount);
            LoadOut.Instance.SetBalance(m_Balance);
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
        }

        private void Start()
        {
            var amount = GETMoneyStack();
            balanceText.text = (m_Balance + amount).ToString();
            m_Balance = m_Balance + amount;
            LoadOut.Instance.SetBalance(m_Balance);

            var fields = GETFieldCount();
            fieldCost.text = m_FieldCosts[fields-1].ToString();
        }

        private void Update()
        {
            m_Balance = myInventory.Inventory[0].StackSize;
            balanceText.text = m_Balance.ToString();
        }

        public void BuyField()
        {
            //Buy a new field
            var nFields = GETFieldCount()-1;
            m_Balance = myInventory.Inventory[0].StackSize;
            if (m_Balance >= m_FieldCosts[nFields])
            {
                ChangeBalance(-1*m_FieldCosts[nFields]);
                myForest[nFields+1].SetActive();
                fieldCost.text = m_FieldCosts[GETFieldCount()-1].ToString();
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
            //Get Value of seed conversion
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
            //Convert seeds to coins
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

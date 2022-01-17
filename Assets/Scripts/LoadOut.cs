using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class LoadOut : MonoBehaviour
    {
        /*
         * User Loadout class
         */
        public static LoadOut Instance { get; private set; }

        private Image m_Img = null;

        public Text quantityLabel;

        public Text itemLabel;

        public Text balanceText;
        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            m_Img = Instance.GetComponent<Image>();
        }

        // Update is called once per frame
        public void SetSprite(Sprite newSprite)
        {
            m_Img.sprite = newSprite;
        }

        public void SetQuantity(int quantity)
        {
            quantityLabel.text = quantity.ToString();
        }

        public void SetItemName(string name)
        {
            itemLabel.text = name;
        }

        public void SetBalance(int balance)
        {
            balanceText.text = balance.ToString();
        }
    }
}

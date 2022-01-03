using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class LoadOut : MonoBehaviour
    {
        public static LoadOut Instance { get; private set; }

        private Image _img = null;

        public Text quantityLabel;

        public Text itemLabel;
        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _img = Instance.GetComponent<Image>();
        }

        // Update is called once per frame
        public void SetSprite(Sprite newSprite)
        {
            _img.sprite = newSprite;
        }

        public void SetQuantity(int quantity)
        {
            quantityLabel.text = quantity.ToString();
        }

        public void SetItemName(string name)
        {
            itemLabel.text = name;
        }
    }
}

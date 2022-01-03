using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class PollutionBar : MonoBehaviour
    {
        public static PollutionBar Instance { get; private set; }
        public Image mask;

        private int _pollution = 100;

        private float _originalSize;
        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
        }

        // Update is called once per frame
        void Start()
        {
            _originalSize = mask.rectTransform.rect.width;
        }

        public void PlantTree()
        {
            _pollution = _pollution - 1;
            SetValue(_pollution/100f);
        }
    
        void SetValue(float value)
        {
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _originalSize * value);
        }
    }
}

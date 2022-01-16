using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class PollutionBar : MonoBehaviour
    {
        public static PollutionBar Instance { get; private set; }
        public Image mask;
        
        public GameObject loseScreen;
        public ParticleSystem FactoryClouds;
        private int _pollution = 0;

        private float _originalSize;
        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
            _originalSize = mask.rectTransform.rect.width;
        }
        
        public void SetPollution(float pollution)
        {
            // Debug.Log("P="+pollution+"/200");
            if (pollution < 200)
            {
                var rot = pollution*0.5f;
                float rod = pollution/10;
                
                var emission = FactoryClouds.emission;
                emission.rateOverTime = rot;
                emission.rateOverDistance = rod;
                
                SetValue(pollution/200f);
            }
            else
            {
                if (pollution > 1)
                {
                    loseScreen.SetActive(true);
                }
                SetValue(1/200f);
            }
        }
    
        private void SetValue(float value)
        {
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (_originalSize * value));
        }
    }
}

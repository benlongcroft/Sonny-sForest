using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    /*
     * Pollution Bar and Smoke Controller
     */
    public class PollutionBar : MonoBehaviour
    {
        public static PollutionBar Instance { get; private set; }
        public Image mask;
        
        public GameObject loseScreen;
        public ParticleSystem factoryClouds;
        private int m_Pollution = 0;

        private float m_OriginalSize;
        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
            m_OriginalSize = mask.rectTransform.rect.width;
        }
        
        public void SetPollution(float pollution)
        //Sets Pollution level on factory clouds and bar
        {
            if (pollution < 200)
            {
                var rot = pollution*0.5f;
                float rod = pollution/10;
                
                var emission = factoryClouds.emission;
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
            //sets value of bar
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (m_OriginalSize * value));
        }
    }
}

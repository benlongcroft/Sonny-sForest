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
        public Text loseScreenTime;
        public Image mask;

        public float Pollution { get; private set; }
        public GameObject loseScreen;
        public ParticleSystem factoryClouds;

        private float m_OriginalSize;
        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
            factoryClouds.Play();
            m_OriginalSize = mask.rectTransform.rect.width;
        }
        
        public void SetPollution(float pollution)
        //Sets Pollution level on factory clouds and bar
        {
            if (pollution < 200)
            {
                Pollution = pollution;
                var rot = pollution*0.5f;
                var rod = pollution/10;
                
                var emission = factoryClouds.emission;
                emission.rateOverTime = rot;
                emission.rateOverDistance = rod;
                Debug.Log("ROT:" +rot);
                Debug.Log("ROD: "+rod);

                SetValue(pollution/200f);
            }
            else
            {
                if (pollution > 1)
                {
                    var t = GameObject.Find("timer").GetComponent<Timer>();
                    t.stop = true;
                    loseScreen.SetActive(true);
                    loseScreenTime.text = t.GETTimer();
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

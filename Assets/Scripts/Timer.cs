using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    /*
     * Controls in-game timer
     */
    private static float m_Timer;
    public bool stop;

    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        m_Timer = 0;
    }

    public string GETTimer()
    {
        return m_Timer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            m_Timer = m_Timer + Time.deltaTime;
            var minutes = (int) (m_Timer / 60);
            var seconds = (int) (m_Timer % 60);
            timerText.text = minutes+":"+seconds; 
        }
    }
}

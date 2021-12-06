using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotController : MonoBehaviour
{
    public GameObject[] subPlotObjs = {};

    private bool[] subPlotStatus = {false, false, false, false};
    // Start is called before the first frame update
    void Start()
    {
    }

    public void growOne()
    {
        for (int i = 0; i <= 4; i = i + 1)
        {
            if (subPlotStatus[i] == false)
            {
                subPlotController g = subPlotObjs[i].GetComponent<subPlotController>();
                g.FlipSeeded();
                subPlotStatus[i] = true;
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

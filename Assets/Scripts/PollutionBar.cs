using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PollutionBar : MonoBehaviour
{
    public static PollutionBar instance { get; private set; }
    public Image mask;

    private int pollution = 100;
    float originalSize;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void PlantTree()
    {
        pollution = pollution - 1;
        SetValue(pollution/100f);
    }
    
    void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}

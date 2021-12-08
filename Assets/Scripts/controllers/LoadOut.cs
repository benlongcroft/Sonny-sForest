using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadOut : MonoBehaviour
{
    public static LoadOut instance { get; private set; }

    private Image img = null;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        img = instance.GetComponent<Image>();
    }

    // Update is called once per frame
    public void SetSprite(Sprite newSprite)
    {
        Debug.Log(newSprite);
        img.sprite = newSprite;
    }
}

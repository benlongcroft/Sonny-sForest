using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subPlotController : MonoBehaviour
{
    public Sprite sml;

    public Sprite med;

    public Sprite lrg;

    public GameObject child;
    
    private SpriteRenderer spriteRenderer;

    protected bool seeded = false;

    private float timer = 0;

    private int currentState = 0;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = child.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = null;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (seeded)
        {
            timer = timer + Time.deltaTime;
            if (timer > 5)
            {
                switch (currentState)
                {
                    case 0:
                        spriteRenderer.sprite = sml;
                        break;
                    case 1:
                        spriteRenderer.sprite = med;
                        break;
                    case 2:
                        spriteRenderer.sprite = lrg;
                        break;
                }

                currentState = currentState + 1;
                timer = 0;
            }
        }    
    }
    public void FlipSeeded()
    {
        seeded = !seeded;
    }
}

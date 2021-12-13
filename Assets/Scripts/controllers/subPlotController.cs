using System;
using UnityEngine;

[Serializable]
public class subPlotController : Plot
{
    public int subPlotID;
    public bool seeded = false;
    public float timer = 0;
    public treeController treeController;
    public GameObject child;
    private SpriteRenderer spriteRenderer;
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
                gsoController.saveForestToJson(System.IO.Directory.GetCurrentDirectory(), this);
                switch (currentState)
                {
                    case 0:
                        spriteRenderer.sprite = treeController.seedlingSprite;
                        break;
                    case 1:
                        spriteRenderer.sprite = treeController.saplingSprite;
                        break;
                    case 2:
                        spriteRenderer.sprite = treeController.treeSprite;
                        break;
                    case 3:
                        spriteRenderer.sprite = treeController.ancientSprite;
                        break;
                    case 4:
                        spriteRenderer.sprite = treeController.deadSprite;
                        break;
                }

                currentState = currentState + 1;
                timer = 0;
            }
        }    
    }

    public void SetTree(treeController t)
    {
        treeController = t;
        seeded = true;
        t.location = new[] {subPlotID, plotID, fieldID};
        gsoController.saveForestToJson(System.IO.Directory.GetCurrentDirectory(), this);
        Debug.Log("Tree Planted");

    }
}

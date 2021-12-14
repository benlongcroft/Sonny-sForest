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
        if (treeController != null)
        {
            spriteRenderer.sprite = treeController.setSprite();
            Debug.Log("Set new Sprite! "+treeController.stage);
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        if (seeded) 
        {
            spriteRenderer.sprite = treeController.setSprite();
            timer = timer + Time.deltaTime;
            if (timer > 25)
            {
                switch (currentState)
                {
                    case 0:
                        spriteRenderer.sprite = treeController.seedlingSprite;
                        treeController.stage = "seedling";
                        break;
                    case 1:
                        spriteRenderer.sprite = treeController.saplingSprite;
                        treeController.stage = "sapling";
                        break;
                    case 2:
                        spriteRenderer.sprite = treeController.treeSprite;
                        treeController.stage = "tree";
                        break;
                    case 3:
                        spriteRenderer.sprite = treeController.ancientSprite;
                        treeController.stage = "ancient";
                        break;
                    case 4:
                        spriteRenderer.sprite = treeController.deadSprite;
                        treeController.stage = "dead";
                        break;
                }
                gsoController.UpdateTree(System.IO.Directory.GetCurrentDirectory(), this);
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
        t.stage = "seed";
        gsoController.SaveNewTree(System.IO.Directory.GetCurrentDirectory(), this);
        Debug.Log("Tree Planted");
    }
}

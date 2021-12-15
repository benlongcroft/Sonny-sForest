using System;
using UnityEngine;

[Serializable]
public class treeController : MonoBehaviour
{
    public int[] location = new int[3];
    public string stage; //seed-seedling-sapling-tree-ancient-dead
    public int hardiness;//%
    public int pollutionThreshold; //%
    public int lifespan; //hours
    public int seedGrowthTime; //hours
    public int diseaseResistance; //% (usually low)
    public string treeType;
    public bool active = false;
    public float efficiency;

    public Sprite seedSprite;

    public Sprite seedlingSprite;

    public Sprite saplingSprite;

    public Sprite treeSprite;

    public Sprite ancientSprite;

    public Sprite deadSprite;

    public Sprite setSprite()
    {
        Debug.Log("Sprite set is "+this.stage);
        switch (stage)
        {
            case "seed":
                return seedSprite;
            case "seedling":
                return seedlingSprite;
            case "sapling":
                return saplingSprite;
            case "tree":
                return treeSprite;
            case "ancient":
                return ancientSprite;
            case "dead":
                return deadSprite;
        }

        return null;
    }
}
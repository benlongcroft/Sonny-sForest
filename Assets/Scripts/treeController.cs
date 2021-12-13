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

    public Sprite seedSprite;

    public Sprite seedlingSprite;

    public Sprite saplingSprite;

    public Sprite treeSprite;

    public Sprite ancientSprite;

    public Sprite deadSprite;
    
}
using System;
using UnityEngine;

[Serializable]
public class Tree : MonoBehaviour
{
    public string stage; //seed-seedling-sapling-tree-ancient-dead
    public int hardiness; //%
    public int pollutionThreshold; //%
    public int lifespan; //hours
    public int seedGrowthTime; //hours
    public int diseaseResistance; //% (usually low)
    
    public bool active = false;

    public Sprite seedSprite;
    public Sprite seedlingSprite;
    public Sprite saplingSprite;
    public Sprite treeSprite;
    public Sprite ancientSprite;
    public Sprite deadSprite;

    public override string ToString()
    {
        return stage + "-" + hardiness + "-" + pollutionThreshold + "-" + lifespan + "-" + seedGrowthTime + "-" +
               diseaseResistance;
    }
}
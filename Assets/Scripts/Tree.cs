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

    private string path;

    public Sprite seedSprite;
    public Sprite seedlingSprite;
    public Sprite saplingSprite;
    public Sprite treeSprite;
    public Sprite ancientSprite;
    public Sprite deadSprite;
    public Tree(int hardiness, int pollutionThreshold, int lifespan, int seedGrowthTime, int diseaseResistance, string path)
    {
        this.hardiness = hardiness;
        this.pollutionThreshold = pollutionThreshold;
        this.lifespan = lifespan;
        this.seedGrowthTime = seedGrowthTime;
        this.diseaseResistance = diseaseResistance;
        this.stage = "seed";
        this.path = path;
    }
        
    public override string ToString()
    {
        return stage + "-" + hardiness + "-" + pollutionThreshold + "-" + lifespan + "-" + seedGrowthTime + "-" +
               diseaseResistance;
    }
}
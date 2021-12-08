using System;

[Serializable]
public class Tree
{
    public string stage; //seedling-sapling-tree-ancient
    public int hardiness; //%
    public int pollutionThreshold; //%
    public int lifespan; //hours
    public int seedGrowthTime; //hours
    public int diseaseResistance; //% (usually low)

    private string path;
    public Tree(int hardiness, int pollutionThreshold, int lifespan, int seedGrowthTime, int diseaseResistance, string path)
    {
        this.hardiness = hardiness;
        this.pollutionThreshold = pollutionThreshold;
        this.lifespan = lifespan;
        this.seedGrowthTime = seedGrowthTime;
        this.diseaseResistance = diseaseResistance;
        this.stage = "seedling";
        this.path = path;
    }
        
    public override string ToString()
    {
        return stage + "-" + hardiness + "-" + pollutionThreshold + "-" + lifespan + "-" + seedGrowthTime + "-" +
               diseaseResistance;
    }
}
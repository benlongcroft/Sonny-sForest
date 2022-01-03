using System;
using UnityEngine;

namespace Forest
{
    [Serializable]
    public class TreeController : MonoBehaviour
    {
        public int[] location = new int[3];
        public string stage; //seed-seedling-sapling-tree-ancient-dead
        public int hardiness;//%
        public int pollutionThreshold; //%
        public int lifespan; //hours
        public int seedGrowthTime; //hours
        public int diseaseResistance; //% (usually low)
        public string treeType;
        public bool active;
        public float efficiency;

        public GameObject seedPrefab;
        
        public Sprite seedSprite;

        public Sprite seedlingSprite;

        public Sprite saplingSprite;

        public Sprite treeSprite;

        public Sprite ancientSprite;

        public Sprite deadSprite;

        public void GrowSeed(int droppedSeeds)
        {
            if (droppedSeeds == 0)
            {
                seedPrefab.SetActive(true);
                seedPrefab.name = "seed-" + droppedSeeds;
            }
            else
            {
                GameObject g = Instantiate(seedPrefab, seedPrefab.transform.position + Vector3.down * 0.4f,
                    Quaternion.identity);
                g.name = "seed-" + droppedSeeds;
                g.SetActive(true);
            }
        }
        
        public Sprite SetSprite()
        {
            Debug.Log("Sprite set is "+stage);
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
                    active = false;
                    return deadSprite;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stage));
            }
        }
    }
}
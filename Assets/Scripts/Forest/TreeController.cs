using System;
using Inventory;
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

        public InventoryItemData seedItem;
        
        public Sprite seedSprite;

        public Sprite seedlingSprite;

        public Sprite saplingSprite;

        public Sprite treeSprite;

        public Sprite ancientSprite;

        public Sprite deadSprite;

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
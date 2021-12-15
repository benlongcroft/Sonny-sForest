using System;
using UnityEngine;
using Main;

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
        public bool active = false;
        public float efficiency;

        public SpriteRenderer spriteRenderer;
        
        public Sprite seedSprite;

        public Sprite seedlingSprite;

        public Sprite saplingSprite;

        public Sprite treeSprite;

        public Sprite ancientSprite;

        public Sprite deadSprite;

        public Sprite SetSprite()
        {
            Debug.Log("Sprite set is "+this.stage);
            return stage switch
            {
                "seed" => spriteRenderer.sprite = seedSprite,
                "seedling" => spriteRenderer.sprite = seedlingSprite,
                "sapling" => spriteRenderer.sprite = saplingSprite,
                "tree" => spriteRenderer.sprite = treeSprite,
                "ancient" => spriteRenderer.sprite = ancientSprite,
                "dead" => spriteRenderer.sprite = deadSprite,
            };
        }
    }
}
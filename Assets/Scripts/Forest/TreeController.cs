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

        public void SetSprite()
        {
            Debug.Log("Sprite set is "+this.stage);
            switch (stage)
            {
                case "seed":
                    spriteRenderer.sprite = seedSprite;
                    break;
                case "seedling":
                    spriteRenderer.sprite = seedlingSprite;
                    break;
                case "sapling":
                    spriteRenderer.sprite = saplingSprite;
                    break;
                case "tree":
                    spriteRenderer.sprite = treeSprite;
                    break;
                case "ancient":
                    spriteRenderer.sprite = ancientSprite;
                    break;
                case "dead":
                    spriteRenderer.sprite = deadSprite;
                    active = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stage));
            }
        }
    }
}
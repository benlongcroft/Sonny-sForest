using System;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using Random = System.Random;

namespace Forest
{
    /*
     * This controls a subplot, which contains the tree
     */
    [Serializable]
    public class SubPlotController : MonoBehaviour
    {
        public int subPlotID;
        public bool seeded;
        public bool dead;
        public int currentStage;
        
        public float timer;
        public float lastTimer;

        public int seedsDropped; //not necessary without GSO
        public float lastSeedTimer;
        
        private string[] m_Stages = {"seed", "seedling", "sapling", "tree", "ancient", "dead"};

        public InventorySystem myInventory;
        public SpriteRenderer treeSpriteRenderer;
        public TreeController treeController;

        private float GrowTime()
        {
            /*
             * Calculates the grow time of any given tree, at any given stage
             */
            var stageMultipliers = new Dictionary<string, float> { {"seed",0}, {"seedling", 0.1f}, {"sapling",0.2f}, {"tree", 0.3f}, {"ancient", 0.4f}};
            return treeController.lifespan * stageMultipliers[treeController.stage];
        }


        // Update is called once per frame
        void Update()
        {
            if (seeded && !dead)
            { 
                //if tree is planted
                timer = timer + Time.deltaTime;
                float g = GrowTime() + (new Random().Next(-5, 5));
                if (timer - lastTimer >= g)
                {
                    //if grow time for stage has been exceeded
                    currentStage += 1;
                    treeController.stage = m_Stages[currentStage];
                    
                    treeSpriteRenderer.sprite = treeController.SetSprite();

                    if (treeController.stage == "dead")
                    {
                        dead = true;
                    }

                    // GSOController.UpdateTree(System.IO.Directory.GetCurrentDirectory(),this);
                    
                    lastTimer = timer;
                }   
            }
            
            if (treeController.stage == "tree" || treeController.stage == "ancient")
            {
                //seed dropping
                if ((timer - lastSeedTimer) >= treeController.seedGrowthTime)
                {
                    myInventory.Add(treeController.seedItem);
                    seedsDropped += 1;
                    lastSeedTimer = timer;
                }

            }

        }

        public void SetTreeEmpty()
        {
            treeController.efficiency = 0;
            treeController.lifespan = 0;
            treeController.seedGrowthTime = 0;
            treeController.treeType = null;
            
            treeController.seedSprite = null;
            treeController.seedlingSprite = null;
            treeController.saplingSprite = null;
            treeController.treeSprite = null;
            treeController.ancientSprite = null;
            treeController.deadSprite = null;

            treeController.location = null;
            treeController.stage = null;
            treeController.active = false;

            treeController.seedItem = null;
        }

        public void SetTree(TreeController t, int[] loc)
        {
            /*
             * converts seed from loadout into seedling in subplot
             */
            treeController.efficiency = t.efficiency;
            treeController.lifespan = t.lifespan;
            treeController.seedGrowthTime = (int) (treeController.lifespan * 0.7f / 8);
            treeController.treeType = t.treeType;

            treeController.seedSprite = t.seedSprite;
            treeController.seedlingSprite = t.seedlingSprite;
            treeController.saplingSprite = t.saplingSprite;
            treeController.treeSprite = t.treeSprite;
            treeController.ancientSprite = t.ancientSprite;
            treeController.deadSprite = t.deadSprite;

            treeController.location = loc;
            treeController.stage = "seed";
            treeController.active = true;

            treeController.seedItem = t.seedItem;

            Destroy(t.gameObject);
            
            treeSpriteRenderer.sprite = treeController.SetSprite();
            seeded = true;
        }
    }
}

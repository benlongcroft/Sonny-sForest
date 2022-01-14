using System;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using Main;
using Unity.Mathematics;
using Random = System.Random;

namespace Forest
{
    [Serializable]
    public class SubPlotController : MonoBehaviour
    {
        public int subPlotID;
        public bool seeded = false;
        public bool dead = false;
        public int currentStage = 0;
        
        public float timer = 0;
        public float lastTimer = 0;

        public int seedsDropped = 0;
        public float lastSeedTimer = 0;
        
        private string[] _stages = {"seed", "seedling", "sapling", "tree", "ancient", "dead"};

        public InventorySystem myInventory;
        public SpriteRenderer treeSpriteRenderer;
        public TreeController treeController;

        private float GrowTime()
        {
            var stageMultipliers = new Dictionary<string, float>(){ {"seed",0}, {"seedling", 0.1f}, {"sapling",0.2f}, {"tree", 0.3f}, {"ancient", 0.4f}};
            return treeController.lifespan * stageMultipliers[treeController.stage];
            //key error here?
        }


        // Update is called once per frame
        void Update()
        {
            if (seeded && !dead)
            { 
                timer = timer + Time.deltaTime;
                float g = GrowTime();
                // Debug.Log((timer - lastTimer)+"/"+g);
                if (timer - lastTimer >= g)
                {
                    currentStage += 1;
                    treeController.stage = _stages[currentStage];
                    
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
                // Debug.Log(timer-lastSeedTimer+"/"+treeController.seedGrowthTime);
                if ((timer - lastSeedTimer) >= treeController.seedGrowthTime)
                {
                    Debug.Log("Added new seed!");
                    myInventory.Add(treeController.seedItem);
                    seedsDropped += 1;
                    lastSeedTimer = timer;
                }

            }

        }

        public void SetTree(TreeController t, int[] loc)
        {
            treeController.efficiency = t.efficiency;
            treeController.hardiness = t.hardiness;
            treeController.lifespan = t.lifespan;
            treeController.diseaseResistance = t.diseaseResistance;
            treeController.pollutionThreshold = t.pollutionThreshold;
            treeController.seedGrowthTime = (int) (treeController.lifespan * 0.7f / 5);
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

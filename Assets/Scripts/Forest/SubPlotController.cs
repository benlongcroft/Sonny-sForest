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
        public float timer = 0;
        public float lastTimer = 0;
        public bool dead = false;
        public int currentStage = 0;
        private string[] _stages = {"seed", "seedling", "sapling", "tree", "ancient", "dead"};
        public int droppedSeeds = 0;

        public InventorySystem myInventory;
        public SpriteRenderer treeSpriteRenderer;
        public TreeController treeController;

        private float GrowTime()
        {
            var stageMultipliers = new Dictionary<string, int>(){ {"seed",0}, {"seedling", 1}, {"sapling",2}, {"tree", 3}, {"ancient", 5}, {"dead", 0}};
            float k = (treeController.lifespan / 12);
            return stageMultipliers[treeController.stage] * k;
            //key error here?
        }


        // Update is called once per frame
        void Update()
        {
            if (seeded && !dead)
            { 
                timer = timer + Time.deltaTime;
                float g = GrowTime();
                Debug.Log((timer - lastTimer)+"/"+g);
                if (timer - lastTimer >= g)
                {
                    currentStage += 1;
                    treeController.stage = _stages[currentStage];
                    
                    treeSpriteRenderer.sprite = treeController.SetSprite();

                    if (treeController.stage == "dead")
                    {
                        dead = true;
                        return;
                    }
                    
                    if (treeController.stage == "tree" || treeController.stage == "ancient")
                    {
                        // treeController.GrowSeed(droppedSeeds);
                        myInventory.Add(treeController.seedItem);
                    }
                    
                    GSOController.UpdateTree(System.IO.Directory.GetCurrentDirectory(),this);
                    
                    lastTimer = timer;
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
            treeController.seedGrowthTime = t.seedGrowthTime;
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
            // treeController.seedPrefab = Instantiate(t.gameObject, treeController.transform.position + Vector3.down * 0.4f, Quaternion.identity);
            // treeController.seedPrefab.SetActive(false);

            Destroy(t.gameObject);
            
            treeSpriteRenderer.sprite = treeController.SetSprite();
            seeded = true;
        }
    }
}

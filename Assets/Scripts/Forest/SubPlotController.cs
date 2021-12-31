using System;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using Main;
using Unity.Mathematics;

namespace Forest
{
    [Serializable]
    public class SubPlotController : MonoBehaviour
    {
        public int subPlotID;
        public bool seeded = false;
        public float timer = 0;
        private float _lastTimer = 0;
        public bool dead = false;
        public int currentStage = 0;
        private string[] _stages = {"seed", "seedling", "sapling", "tree", "ancient", "dead"};
        // public TreeController treeController;
        public SpriteRenderer treeSpriteRenderer;
        public TreeController treeController;

        private float GrowTime()
        {
            var stageMultipliers = new Dictionary<string, int>(){ {"seed",1}, {"seedling", 1}, {"sapling",2}, {"tree", 3}, {"ancient", 5}, {"dead", 0}};
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
                Debug.Log((timer - _lastTimer)+"/"+g);
                if (timer - _lastTimer >= g)
                {
                    treeSpriteRenderer.sprite = treeController.SetSprite();
                    if (treeController.stage == "dead")
                    {
                        dead = true;
                        return;
                    } 
                    
                    if (treeController.stage == "tree" || treeController.stage == "ancient")
                    {
                        GameObject newSeed = Instantiate(treeController.seedPrefab);
                        newSeed.name = "seedDropped";
                        newSeed.layer = 0;
                        newSeed.SetActive(true);
                    }
                    
                    GSOController.UpdateTree(System.IO.Directory.GetCurrentDirectory(),this);
                    
                    if (treeController.stage == _stages[currentStage])
                    {
                        treeController.stage = _stages[currentStage + 1];
                        currentStage += 1;
                    }
                    else
                    {
                        Debug.Log("NOT MATCHING: "+treeController.stage+"--"+_stages[currentStage]);
                    }
                    _lastTimer = timer;
                }   
            }

        }

        public void SetTree(TreeController t, int[] loc)
        {
            treeController = Instantiate(t);
            treeController.seedPrefab = t.gameObject;
            treeController.location = loc;
            treeController.stage = "seed";
            treeController.active = true;
            seeded = true;
        }
    }
}

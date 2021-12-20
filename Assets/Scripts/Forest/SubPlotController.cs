using System;
using System.Collections.Generic;
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
        public int currentStage = 0;
        private string[] _stages = {"seed", "seedling", "sapling", "tree", "ancient", "dead"};
        public TreeController treeController;

        private float GrowTime()
        {
            var stageMultipliers = new Dictionary<string, int>(){ {"seed",1}, {"seedling", 1}, {"sapling",2}, {"tree", 3}, {"ancient", 5}};
            float k = (treeController.lifespan / 12);
            return stageMultipliers[treeController.stage] * k;
            //key error here?
        }


        // Update is called once per frame
        void Update()
        {
            if (seeded)
            { 
                timer = timer + Time.deltaTime;
                float g = GrowTime();
                // Debug.Log((timer - _lastTimer)+"/"+g);
                if (timer - _lastTimer >= g)
                {
                    treeController.SetSprite();
                    if (treeController.stage == "dead")
                    {
                        seeded = false;
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
            t.spriteRenderer = treeController.spriteRenderer;
            //swap around sprite renderer
            
            treeController = t;
            treeController.location = loc;
            treeController.stage = "seed";
            treeController.active = true;
            seeded = true;
        }
    }
}

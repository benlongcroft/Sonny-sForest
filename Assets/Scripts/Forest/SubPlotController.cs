using System;
using System.Collections.Generic;
using UnityEngine;
using Main;
using Unity.Mathematics;

namespace Forest
{
    [Serializable]
    public class SubPlotController : Plot
    {
        public int subPlotID;
        public bool seeded = false;
        public float timer = 0;
        private float _lastTimer = 0;
        private string[] _stages = new[] {"seed", "seedling", "sapling", "tree", "ancient", "dead"};
        public int currentStage = 0;
        public TreeController treeController;

        private float GrowTime()
        {
            var stageMultipliers = new Dictionary<string, int>(){ {"seed",1}, {"seedling", 1}, {"sapling",2}, {"tree", 3}, {"ancient", 5}};
            float k = (treeController.lifespan / 12);
            return stageMultipliers[treeController.stage] * k;
        }


        // Update is called once per frame
        void Update()
        {
            if (seeded)
            { 
                timer = timer + Time.deltaTime;
                float g = GrowTime();
                Debug.Log((timer - _lastTimer)+"/"+g);
                if (timer - _lastTimer >= g)
                {
                    treeController.SetSprite();
                    treeController.stage = _stages[currentStage];
                    currentStage += 1;
                    GSOController.UpdateTree(System.IO.Directory.GetCurrentDirectory(),this);
                    _lastTimer = timer;
                }   
            }

        }

        public void SetTree(TreeController t)
        {
            treeController = t;
            seeded = true;
            treeController.active = true;
        }
    }
}

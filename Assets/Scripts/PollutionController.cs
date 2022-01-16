using System;
using System.Collections;
using System.Collections.Generic;
using Forest;
using Main;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class PollutionController : MonoBehaviour
    {
        private float _pollutionCount = 0;
        private float _previousPollution = 10;
        float totalEfficiency = 0f;
        private int treeCount = 0;
        private int previousTreeCount = 0;
        
        private bool _generatorTickStarted = false;
        private Field[] _currentForest;
        private float _currentEfficiency = 0;
        private int _currentTreeCount = 0;
        private DateTime _forestGenerationTime = DateTime.Now;
        private PollutionBar pbar = PollutionBar.Instance;
    
        // Start is called before the first frame update
        void Start()
        {
            _currentForest = GameObject.Find("char").GetComponent<CharController>().myForest;
            StartCoroutine(GeneratorTick());
        }

        // Update is called once per frame
        void Update()
        {
        }
        
        private IEnumerator GeneratorTick()
        {
            if (!_generatorTickStarted)
            {
               _generatorTickStarted = true;
               SetEfficiencyAndTreeCount();
               TimeSpan timeSinceCreation = DateTime.Now - _forestGenerationTime;
               var unlockedFieldCount = CalculateUnlockedFieldCount();
               
               var f = 1.0f + (0.05f * (unlockedFieldCount - 1));
               var t = totalEfficiency / (22 * unlockedFieldCount);
               // calculate pollution count using formula created by Ben 
               _pollutionCount = _previousPollution + (f - t);
               PollutionBar.Instance.SetPollution(_pollutionCount);
               Debug.Log("P="+_pollutionCount+" E="+totalEfficiency+" PP="+_previousPollution);
               if (_pollutionCount < 0)
               {
                   _previousPollution = 0;
               }
               else
               {
                   _previousPollution = _pollutionCount;
               }
            }
            
            yield return new WaitForSeconds(1);
            _generatorTickStarted = false;
            StartCoroutine(GeneratorTick());
            
        }
        
        private int CalculateUnlockedFieldCount()
        {
            int availFieldCount = 0;
            
            foreach (Field indivField in _currentForest)
            {
                if (indivField.unlocked)
                {
                    availFieldCount++;
                }
            }

            return availFieldCount;
        }

        private void SetEfficiencyAndTreeCount()
        {
            treeCount = 0;
            totalEfficiency = 0;
            Dictionary<string, float> eMult = new Dictionary<string, float>()
            {
                {"seed", 0f},
                {"seedling", 0.6f},
                {"sapling", 0.7f},
                {"tree", 0.9f},
                {"ancient", 1f}
            };

            foreach (Field indivField in _currentForest)
            {
                foreach (Plot indivPlot in indivField.plots)
                {
                    foreach (SubPlotController indivSpc in indivPlot.subPlots)
                    {
                        if (indivSpc.seeded && !indivSpc.dead)
                        {
                            totalEfficiency += indivSpc.treeController.efficiency *
                                               eMult[indivSpc.treeController.stage];
                            treeCount++;
                        }
                    }
                }
            }

            if (previousTreeCount < treeCount)
            {
                for (var _ = 0; _ < (treeCount - previousTreeCount); _++)
                {
                    if (_previousPollution > 1 && _pollutionCount > 1)
                    {
                        _previousPollution-=0.8f;
                    }
                }
            }

            previousTreeCount = treeCount;
        }
        //     if (totalTreeCount != 0)
        //     {
        //         _currentEfficiency = totalEfficiency / totalTreeCount;
        //     }
        //     else
        //     {
        //         _currentEfficiency = 0;
        //     }
        //     
        //     _currentTreeCount = totalTreeCount;
        // }
    }
    
}

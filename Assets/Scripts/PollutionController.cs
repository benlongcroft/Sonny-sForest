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
               int unlockedFieldCount = CalculateUnlockedFieldCount();
               
               // calculate pollution count using formula created by Ben 
               _pollutionCount = ((float) timeSinceCreation.TotalSeconds) * 
                   ((1f + (0.5f * unlockedFieldCount)) - (_currentTreeCount / 15) * _currentEfficiency) + 10;
               PollutionBar.Instance.SetPollution(_pollutionCount);
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

            return availFieldCount-1;
        }
        
        private void SetEfficiencyAndTreeCount()
        {
            int totalTreeCount = 0;
            float totalEfficiency = 0;
            Dictionary<string, float> eMult = new Dictionary<string, float>() { {"seed", 0f},
                                                                                {"seedling", 0.2f}, 
                                                                                {"sapling", 0.5f},
                                                                                {"tree", 0.8f},
                                                                                {"ancient", 1f}};

            foreach (Field indivField in _currentForest)
            {
                foreach (Plot indivPlot in indivField.plots)
                {
                    foreach (SubPlotController indivSpc in indivPlot.subPlots)
                    {
                        if (indivSpc.seeded && !indivSpc.dead)
                        {
                            totalTreeCount++;
                            totalEfficiency += indivSpc.treeController.efficiency * eMult[indivSpc.treeController.stage];
                        }
                    }
                }
            }

            if (totalTreeCount != 0)
            {
                _currentEfficiency = totalEfficiency / totalTreeCount;
            }
            else
            {
                _currentEfficiency = 0;
            }
            
            _currentTreeCount = totalTreeCount;
        }
    }
    
}

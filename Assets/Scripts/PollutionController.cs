using System;
using System.Collections;
using Forest;
using Main;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class PollutionController : MonoBehaviour
    {
        private float _pollutionCount = 0;
        private bool _generatorTickStarted = false;
        private Field[] _currentForest;
        private float _currentEfficiency = 0;
        private int _currentTreeCount = 0;
        private DateTime _forestGenerationTime = DateTime.Now;
    
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
                   ((1 + .4f * unlockedFieldCount) - (_currentTreeCount / 24) * _currentEfficiency) + 10;
               Debug.Log("Finished pollution generation at timestamp : " + Time.time 
                                                                      + " e=" + _currentEfficiency 
                                                                      + " treeCount=" + _currentTreeCount
                                                                      + " unlockedFields=" + unlockedFieldCount);
            }
            
            yield return new WaitForSeconds(1);
            
            _generatorTickStarted = false;
            StartCoroutine(GeneratorTick());
            
            Debug.Log("Pollution=" + _pollutionCount);
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
            int totalTreeCount = 0;
            float totalEfficiency = 0;
            
            Debug.Log("Start efficiency count at timestamp : " + Time.time);
            
            foreach (Field indivField in _currentForest)
            {
                foreach (Plot indivPlot in indivField.plots)
                {
                    foreach (SubPlotController indivSpc in indivPlot.subPlots)
                    {
                        if (indivSpc.seeded && !indivSpc.dead)
                        {
                            totalTreeCount++;
                            totalEfficiency += indivSpc.treeController.efficiency;
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

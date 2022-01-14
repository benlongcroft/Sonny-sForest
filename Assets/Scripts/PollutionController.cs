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
        [SerializeField]
        private Text _pollutionDisplayText;
        
        private float _pollutionCount = 0;
        private bool _generatorTickStarted = false;
        private Field[] _currentForest;
        private float _currentEfficiency = 0;
        private int _currentTreeCount = 0;
        private DateTime _forestGenerationTime = DateTime.Now;
    
        // Start is called before the first frame update
        void Start()
        {
            _pollutionDisplayText = GetComponentInChildren<Text>();
            _currentForest = GameObject.Find("char").GetComponent<CharController>().myForest;
            StartCoroutine(GeneratorTick());
        }

        // Update is called once per frame
        void Update()
        {
            _pollutionDisplayText.text = _pollutionCount.ToString();
        }
        
        private IEnumerator GeneratorTick()
        {
            if (!_generatorTickStarted)
            {
               _generatorTickStarted = true;
               SetEfficiencyAndTreeCount();
               TimeSpan timeSinceCreation = DateTime.Now - _forestGenerationTime;

               // calculate pollution count using formula created by Ben 
               _pollutionCount = ((float) timeSinceCreation.TotalHours) * (1 - _currentEfficiency) + 10;
            }

            //Print the time of when the function is first called.
            Debug.Log("Started Coroutine at timestamp : " + Time.time);
 
            //yield on a new YieldInstruction that waits for 1 second.
            yield return new WaitForSeconds(1);
            _generatorTickStarted = false;

            //After we have waited 1 second print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            StartCoroutine(GeneratorTick());
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
                        if (indivSpc.treeController.active)
                        {
                            totalTreeCount++;
                            totalEfficiency = indivSpc.treeController.efficiency;
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
            
            Debug.Log("Finished  efficiency count at timestamp : " + Time.time 
            + " e=" + _currentEfficiency + " treeCount=" + _currentTreeCount);
        }
    }
    
}

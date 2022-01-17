using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Forest;
using Main;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    /*
     * Main Pollution calculator
     */
    public class PollutionController : MonoBehaviour
    {
        private float m_PollutionCount = 0;
        private float m_PreviousPollution = 10;
        float m_TotalEfficiency = 0f;
        private int m_TreeCount = 0;
        private int m_PreviousTreeCount = 0;
        
        private bool m_GeneratorTickStarted = false;
        private Field[] m_CurrentForest;

        // Start is called before the first frame update
        private void Start()
        {
            m_CurrentForest = GameObject.Find("char").GetComponent<CharController>().myForest;
            //get forest
            StartCoroutine(GeneratorTick());
        }
        private IEnumerator GeneratorTick()
        //calculate new pollution value
        {
            if (!m_GeneratorTickStarted)
            {
               m_GeneratorTickStarted = true;
               SetEfficiencyAndTreeCount();
               var unlockedFieldCount = CalculateUnlockedFieldCount();
               
               //difficulty
               var f = 1.0f + (0.05f * (unlockedFieldCount - 1));
               
               //Field Score
               var t = m_TotalEfficiency / (22 * unlockedFieldCount);
               
               // calculate pollution count using formula created by Ben 
               m_PollutionCount = m_PreviousPollution + (f - t);
               
               PollutionBar.Instance.SetPollution(m_PollutionCount);
               
               if (m_PollutionCount < 0)
               {
                   m_PreviousPollution = 0;
               }
               else
               {
                   m_PreviousPollution = m_PollutionCount;
               }
            }
            
            yield return new WaitForSeconds(1);
            m_GeneratorTickStarted = false;
            StartCoroutine(GeneratorTick());
            
        }
        
        private int CalculateUnlockedFieldCount()
        {
            return m_CurrentForest.Count(indivField => indivField.Unlocked);
        }

        private void SetEfficiencyAndTreeCount()
        {
            m_TreeCount = 0;
            m_TotalEfficiency = 0;
            Dictionary<string, float> eMult = new Dictionary<string, float>()
            {
                {"seed", 0f},
                {"seedling", 0.6f},
                {"sapling", 0.75f},
                {"tree", 0.9f},
                {"ancient", 1f}
            };

            foreach (var indivField in m_CurrentForest)
            {
                foreach (var indivPlot in indivField.plots)
                {
                    foreach (var indivSpc in indivPlot.subPlots.Where(indivSpc => indivSpc.seeded && !indivSpc.dead))
                    {
                        m_TotalEfficiency += indivSpc.treeController.efficiency *
                                             eMult[indivSpc.treeController.stage];
                        m_TreeCount++;
                    }
                }
            }

            
            //tree plant bonus (decreases pollution by 1 for each planted tree)
            if (m_PreviousTreeCount < m_TreeCount)
            {
                for (var _ = 0; _ < (m_TreeCount - m_PreviousTreeCount); _++)
                {
                    if (m_PreviousPollution > 1 && m_PollutionCount > 1)
                    {
                        m_PreviousPollution-=1.0f;
                    }
                }
            }

            m_PreviousTreeCount = m_TreeCount;
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

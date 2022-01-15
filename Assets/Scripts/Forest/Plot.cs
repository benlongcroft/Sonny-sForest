using System.Collections.Generic;
using AOT;
using Main;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Forest
{
    public class Plot : MonoBehaviour
    {
        public Field field;
        
        public List<SubPlotController> subPlots = new List<SubPlotController> {};
    
        public int plotID;

        // void Update()
        // {
        //     for (var i = 0; i < 4; i += 1)
        //     {
        //         if (subPlots[i].treeController.stage == "tree" || subPlots[i].treeController.stage == "ancient")
        //         {
        //             droppedSeeds.Add(Instantiate(TreeController));
        //         }
        //     }
        // }
    
        public int ChoosePlot(TreeController treeControllerObj)
        {
            for(var i =0; i < 4; i ++)
            {
                if (subPlots[i].dead) continue;
                if (subPlots[i].seeded) continue;
                subPlots[i].SetTree(treeControllerObj, new[] {field.fieldID, plotID, subPlots[i].subPlotID});
                // GSOController.SaveNewTree(System.IO.Directory.GetCurrentDirectory(), subPlots[i]);
                return i;
            }
            return -1;
        }

        public int ChopDownTree()
        {
            for (var i = 0; i < 4; i++)
            {
                if (subPlots[i].dead)
                {
                    subPlots[i].SetTreeEmpty();
                    subPlots[i].treeSpriteRenderer.sprite = null;
                    subPlots[i].seeded = false;
                    subPlots[i].timer = 0;
                    subPlots[i].lastTimer = 0;
                    subPlots[i].seedsDropped = 0;
                    subPlots[i].lastSeedTimer = 0;
                    subPlots[i].currentStage = 0;
                    subPlots[i].dead = false;
                    return i;
                }
            }

            return -1;
        }
    }
}

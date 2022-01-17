using System.Collections.Generic;
using UnityEngine;

namespace Forest
{
    public class Plot : MonoBehaviour
    /*
     * Plot contains four subplots. It is part of a field.
     */
    {
        public Field field;
        
        public List<SubPlotController> subPlots = new List<SubPlotController> {};
    
        public int plotID;
        
    
        public int ChoosePlot(TreeController treeControllerObj)
        /*
         * When Sonny attempts to plant a seed by a plot, this chooses the most available plot.
         */
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
        /*
         * When Sonny attempts to chop down a dead tree, this chooses the tree to chop down
         */
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

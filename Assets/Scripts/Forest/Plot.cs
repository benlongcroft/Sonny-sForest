using System.Collections.Generic;
using Main;

namespace Forest
{
    public class Plot : Field
    {

        public List<SubPlotController> subPlots = new List<SubPlotController> {};
    
        public int plotID;

        // void Update()
        // {
        //     for (var i = 0; i < 4; i += 1)
        //     {
        //         if (trees[i].stage != null && subPlots[i].seeded == false)
        //         {
        //             trees[i].location = new[] {fieldID, plotID, subPlots[i].subPlotID};
        //             trees[i].spriteRenderer = subPlots[i].treeController.spriteRenderer;
        //             //swap around sprite renderer so that tree grows in subplot
        //             subPlots[i].treeController = trees[i];
        //             subPlots[i].treeController.active = true;
        //         
        //             GSOController.SaveNewTree(System.IO.Directory.GetCurrentDirectory(), subPlots[i]);   
        //         }
        //     }
        // }
    
        public int ChoosePlot(TreeController treeControllerObj)
        {
            for(var i =0; i < 4; i += 1)
            {
                if (subPlots[i].seeded == false && subPlots[i].dead == false)
                {
                    subPlots[i].SetTree(treeControllerObj, new[] {fieldID, plotID, subPlots[i].subPlotID});
                    GSOController.SaveNewTree(System.IO.Directory.GetCurrentDirectory(), subPlots[i]);
                    return i;
                }
            }
            return -1;
        }
    }
}

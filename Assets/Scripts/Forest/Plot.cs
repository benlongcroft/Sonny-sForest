using System.Collections.Generic;
using UnityEngine;

public class Plot : Field
{
    public List<treeController> trees = new List<treeController>{};
    
    public List<subPlotController> subPlots = new List<subPlotController> {};
    
    public int plotID;

    void Update()
    {
        for (int i = 0; i < 4; i = i + 1)
        {
            if (trees[i] != null && subPlots[i].seeded == false)
            {
                trees[i].location = new[] {fieldID, plotID, subPlots[i].subPlotID};
                trees[i].stage = "seed";
                subPlots[i].SetTree(trees[i]);
                gsoController.SaveNewTree(System.IO.Directory.GetCurrentDirectory(), subPlots[i]);
            }
        }
    }
    
    public int choosePlot(treeController treeControllerObj)
    {
        for(int i =0; i < 4; i=i+1)
        {
            if (trees[i] == null)
            {
                treeControllerObj.active = true;
                trees[i] = treeControllerObj;
                return i;
            }
        }

        return -1;
    }
}

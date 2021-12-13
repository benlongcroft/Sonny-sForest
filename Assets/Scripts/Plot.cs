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
                subPlots[i].SetTree(trees[i]);
            }
        }
    }
    
    public int choosePlot(treeController treeControllerObj)
    {
        Debug.Log(treeControllerObj.ToString());
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

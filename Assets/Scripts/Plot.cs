using System;
using System.Collections.Generic;
using controllers;
using UnityEngine;

public class Plot : MonoBehaviour
{
    public List<Tree> trees = new List<Tree> {null, null, null, null};
    public List<subPlotController> subPlots = new List<subPlotController> {null, null, null, null};
    private bool empty;
    public Plot(bool empty = true)
    {
        this.empty = empty;
    }

    void Update()
    {
        for (int i = 0; i < 3; i = i + 1)
        {
            if (trees[i] != null && subPlots[i] != null)
            {
                subPlots[i].setTree(trees[i]);
            }
        }
    }

    public void setTree(int subPlotNum, Tree treeObj)
    {
        trees[subPlotNum] = treeObj;
    }

    public bool choosePlot(Tree treeObj)
    {
        for(int i =0; i < 3; i=i+1)
        {
            if (trees[i] == null)
            {
                trees[i] = treeObj;
                return true;
            }
        }

        return false;
    }
}

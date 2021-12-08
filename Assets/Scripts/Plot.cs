using System;
using System.Collections.Generic;
using controllers;
using UnityEngine;

public class Plot : MonoBehaviour
{
    public Tree[] trees = new Tree[4];
    public subPlotController[] subPlots = new subPlotController[4];
    private bool empty;
    public Plot(bool empty = true)
    {
        this.empty = empty;
    }

    void Update()
    {
        for (int i = 0; i < 4; i = i + 1)
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

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
            if (trees[i].active && subPlots[i].seeded == false)
            {
                subPlots[i].SetTree(trees[i]);
            }
        }
    }
    
    public bool choosePlot(Tree treeObj)
    {
        for(int i =0; i < 3; i=i+1)
        {
            if (trees[i].active == false)
            {
                treeObj.active = true;
                trees[i] = treeObj;
                return true;
                break;
            }
        }

        return false;
    }
}

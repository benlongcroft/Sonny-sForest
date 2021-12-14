using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class gsoController
{
    private static List<int[]> getIDs(string manifestPath)
    {
        List<int[]> ids = new List<int[]>();
        string[] sIDs = File.ReadAllLines(manifestPath);
        foreach (string sid in sIDs)
        {
            string[] id = sid.Split('-');
            ids.Add(new int[]{Convert.ToInt32(id[0]), Convert.ToInt32(id[1]), Convert.ToInt32(id[2])}); 
        }
        return ids;
    }

    public static void UpdateTree(string cwd, subPlotController spToUpdate)
    {
        string manifestPath = Path.Combine(cwd, "Assets/GameObjects/manifest.txt");
        int[] loc = spToUpdate.treeController.location;
        if (File.Exists(manifestPath))
        {
            List<int[]> ids = getIDs(manifestPath);
            foreach (int[] id in ids)
            {
                if (id.SequenceEqual(loc))
                {
                    string jsonSp = JsonUtility.ToJson(spToUpdate, true);
                    string jsonT = JsonUtility.ToJson(spToUpdate.treeController, true);
                    File.WriteAllText(Path.Combine(cwd, "Assets/GameObjects/forest/sp_"+loc[0]+loc[1]+loc[2]+".json"), jsonSp);
                    File.WriteAllText(Path.Combine(cwd, "Assets/GameObjects/forest/t_"+loc[0]+loc[1]+loc[2]+".json"), jsonT);
                }
            }
        }
    }
    public static Field[] ReadForest(string cwd, Field[] myForest)
    {
        string manifestPath = Path.Combine(cwd, "Assets/GameObjects/manifest.txt");
        if (File.Exists(manifestPath))
        {
            List<int[]> ids = getIDs(manifestPath);
            foreach (int[] loc in ids)
            {
                if (loc == null)
                {
                    break;
                }
                var jsonSp = File.ReadAllText(Path.Combine(cwd, "Assets/GameObjects/forest/sp_"+loc[0]+loc[1]+loc[2]+".json"));
                var jsonT = File.ReadAllText(Path.Combine(cwd, "Assets/GameObjects/forest/t_"+loc[0]+loc[1]+loc[2]+".json"));
                JsonUtility.FromJsonOverwrite(jsonSp, myForest[loc[0]].plots[loc[1]].subPlots[loc[2]]);
                JsonUtility.FromJsonOverwrite(jsonT, myForest[loc[0]].plots[loc[1]].subPlots[loc[2]].treeController);
                Debug.Log("GSO loaded "+myForest[loc[0]].plots[loc[1]].subPlots[loc[2]].treeController.stage);
            }
        }
        return myForest;
    }

    public static void SaveNewTree(string cwd, subPlotController sp)
    {
        string manifestPath = Path.Combine(cwd, "Assets/GameObjects/manifest.txt");
        int[] loc = sp.treeController.location;
        string line = loc[0] + "-" + loc[1] + "-" + loc[2]+"\n";
        if (File.Exists(manifestPath))
        {
            Debug.Log("Manifest Exists");
            File.AppendAllText(manifestPath, line);
        }
        else
        {
            Debug.Log("Manifest does not exist");
            File.WriteAllText(manifestPath, line);
        }
        string jsonSp = JsonUtility.ToJson(sp, true);
        string jsonT = JsonUtility.ToJson(sp.treeController, true);
        File.WriteAllText(Path.Combine(cwd, "Assets/GameObjects/forest/sp_"+loc[0]+loc[1]+loc[2]+".json"), jsonSp);
        File.WriteAllText(Path.Combine(cwd, "Assets/GameObjects/forest/t_"+loc[0]+loc[1]+loc[2]+".json"), jsonT);
    }

}
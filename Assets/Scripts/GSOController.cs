using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forest;
using UnityEngine;

namespace Main
{
    public static class GSOController
    {
        private static List<int[]> GETIDs(string manifestPath)
        {
            var ids = new List<int[]>();
            var sIDs = File.ReadAllLines(manifestPath);
            foreach (var sid in sIDs)
            {
                var id = sid.Split('-');
                ids.Add(new int[]{Convert.ToInt32(id[0]), Convert.ToInt32(id[1]), Convert.ToInt32(id[2])}); 
            }
            return ids;
        }

        private static void WriteToJson(SubPlotController sp, int[] loc, string cwd)
        {
            var jsonSp = JsonUtility.ToJson(sp, true);
            var jsonT = JsonUtility.ToJson(sp.treeController, true);
            File.WriteAllText(Path.Combine(cwd, "Assets/GameObjects/forest/sp_"+loc[0]+loc[1]+loc[2]+".json"), jsonSp);
            File.WriteAllText(Path.Combine(cwd, "Assets/GameObjects/forest/t_"+loc[0]+loc[1]+loc[2]+".json"), jsonT);
        }

        public static void UpdateTree(string cwd, SubPlotController spToUpdate)
        {
            var manifestPath = Path.Combine(cwd, "Assets/GameObjects/manifest.txt");
            var loc = spToUpdate.treeController.location;
            if (!File.Exists(manifestPath)) return;
            var ids = GETIDs(manifestPath);
            foreach (var id in ids)
            {
                if (!id.SequenceEqual(loc)) continue;
                WriteToJson(spToUpdate, loc, cwd);
            }
        }
        public static Field[] ReadForest(string cwd, Field[] myForest)
        {
            var manifestPath = Path.Combine(cwd, "Assets/GameObjects/manifest.txt");
            if (!File.Exists(manifestPath)) return myForest;
            var ids = GETIDs(manifestPath);
            foreach (var loc in ids)
            {
                if (loc == null)
                {
                    break;
                }
                var jsonSp = File.ReadAllText(Path.Combine(cwd, "Assets/GameObjects/forest/sp_"+loc[0]+loc[1]+loc[2]+".json"));
                var jsonT = File.ReadAllText(Path.Combine(cwd, "Assets/GameObjects/forest/t_"+loc[0]+loc[1]+loc[2]+".json"));
                JsonUtility.FromJsonOverwrite(jsonSp, myForest[loc[0]].plots[loc[1]].subPlots[loc[2]]);
                JsonUtility.FromJsonOverwrite(jsonT, myForest[loc[0]].plots[loc[1]].subPlots[loc[2]].treeController);
                myForest[loc[0]].plots[loc[1]].subPlots[loc[2]].treeController.SetSprite();
                Debug.Log("GSO loaded "+myForest[loc[0]].plots[loc[1]].subPlots[loc[2]].treeController.stage);
            }
            return myForest;
        }

        public static void SaveNewTree(string cwd, SubPlotController sp)
        {
            var manifestPath = Path.Combine(cwd, "Assets/GameObjects/manifest.txt");
            var loc = sp.treeController.location;
            var line = loc[0] + "-" + loc[1] + "-" + loc[2]+"\n";
            if (File.Exists(manifestPath))
            {
                var ids = GETIDs(manifestPath);
                if (!ids.Contains(loc))
                {
                    File.AppendAllText(manifestPath, line);
                }
            }
            else
            {
                Debug.Log("Manifest does not exist");
                File.WriteAllText(manifestPath, line);
            }
            WriteToJson(sp, loc, cwd);
        }

    }

}

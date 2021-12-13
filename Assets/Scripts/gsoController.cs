using System.IO;
using UnityEngine;

public class gsoController
{
    private static int[] getIDs(string path)
    {
        int[] idsOut = new int[3];
        string[] d = path.Split('/');
        //split path 
        string filename = d[d.Length - 1].Split('.')[0];
        //get last item (i.e .json file) and split filename from file extension - get name and store in l
        
        idsOut[0] = int.Parse(filename[0].ToString());
        idsOut[1] = int.Parse(filename[1].ToString());
        idsOut[2] = int.Parse(filename[2].ToString());

        return idsOut;
    }
    private static string[] getGameFiles(string cwd)
    {
        string[] files = new string[10];
        cwd = Path.Combine(cwd, "Assets/GameObjects/forest");
        // string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        DirectoryInfo d = new DirectoryInfo(cwd);
        int x = 0;
        foreach (var file in d.GetFiles("*.json"))
        {
            files[x] = file.FullName;
            x = x + 1;
        }

        return files;
    }
    public static Field[] getForestFromJson(string cwd, Field[] myForest)
    {
        string[] paths = getGameFiles(cwd);
        foreach (string p in paths)
        {
            if (p == null)
            {
                break;
            }

            int[] ids = getIDs(p);
            var jsonString = File.ReadAllText(p);
            JsonUtility.FromJsonOverwrite(jsonString, myForest[ids[0]].plots[ids[1]].subPlots[ids[2]]);
        }
        return myForest;
    }

    public static void saveForestToJson(string cwd, subPlotController sp)
    {
        string json = JsonUtility.ToJson(sp, true); 
        Debug.Log(json);
        string id = sp.treeController.location[0].ToString() + sp.treeController.location[1].ToString() + sp.treeController.location[2].ToString();
        System.IO.File.WriteAllText(Path.Combine(cwd, "/Assets/GameObjects/forest/"+id+".json"), json);
    }

}
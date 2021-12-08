using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class gsoController
{
    public static Forest fromJSON(string path)
    {
        Forest loadedForest= null;
        if (path != null)
        {
            loadedForest = JsonUtility.FromJson<Forest>(path);
        }

        return loadedForest;
    }

    public static void toJSON(Forest forest)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string json = JsonUtility.ToJson(forest);
        Debug.Log(json);
        File.WriteAllText(currentDirectory+"/Assets/GameObjects/forest_test.json", json);
    }

}
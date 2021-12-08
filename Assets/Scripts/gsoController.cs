using System.IO;
using UnityEngine;

public class gsoController
{
    public static global::Tree fromJSON(string path)
    {
        global::Tree loadedTree = null;
        if (path != null)
        {
            loadedTree = JsonUtility.FromJson<global::Tree>(path);
        }

        return loadedTree;
    }

    public static bool toJSON(global::Tree tree)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string json = JsonUtility.ToJson(tree);
        Debug.Log(json);
        File.WriteAllText(currentDirectory+"/Assets/Scripts/tree_test.json", json);
        return true;
    }

}

class main
{
    public static void Main(string[] args)
    {
        var t = new global::Tree(20, 87,30,1,29, "/Users/benlongcroft/Documents/unity/TreeProto/Assets/Scripts");
        gsoController.toJSON(t);
    }
}
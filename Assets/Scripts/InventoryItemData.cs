using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public GameObject prefab;

    public Sprite GetSpriteIcon()
    {
        return prefab.GetComponent<SpriteRenderer>().sprite;
    }
}
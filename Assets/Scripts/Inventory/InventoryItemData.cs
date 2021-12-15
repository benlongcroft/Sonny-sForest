using UnityEngine;

namespace Inventory
{
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
}
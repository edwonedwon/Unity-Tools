using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools 
{
    [CreateAssetMenu(fileName = "Item Storage", menuName = "Scriptables/Item Storage")]
    public class ItemStorageSO : ScriptableObject
    {
        public List<Item> items;

        public GameObject GetItemPrefab(string itemName)
        {
            foreach (Item item in items)
            {
                if (item.itemName == itemName)
                    return item.gameObject;
            }
            Debug.LogWarning("Item Prefab with itemName " + itemName + " was not found in storage");
            return null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools 
{
    [CreateAssetMenu(fileName = "Item Storage", menuName = "Scriptables/Item Storage")]
    public class ItemStorageSO : ScriptableObject
    {
        [System.Serializable]
        public struct ItemSlot
        {
            public Item itemPrefab;
            public int numberInPool;
            public Sprite icon;
        }

        public List<ItemSlot> itemSlots;

        public GameObject GetItemPrefab(string itemName)
        {
            foreach (ItemSlot slot in itemSlots)
            {
                if (slot.itemPrefab.itemName == itemName)
                    return slot.itemPrefab.gameObject;
            }
            Debug.LogWarning("Item Prefab with itemName " + itemName + " was not found in storage");
            return null;
        }

        public Sprite GetItemIcon(string itemName)
        {
            foreach (ItemSlot slot in itemSlots)
            {
                if (slot.itemPrefab.itemName == itemName)
                    return slot.icon;
            }
            Debug.LogWarning("Item icon with itemName " + itemName + " was not found in storage");
            return null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools 
{
    [System.Serializable]
    public class PrefabSlot
    {
        public string itemName;
        public GameObject gameObject;
    }

    [CreateAssetMenu(fileName = "Prefab Storage", menuName = "Scriptables/Prefab Storage")]
    public class PrefabStorageSO : ScriptableObject
    {
        public PrefabSlot[] prefabs;

        public GameObject GetPrefab(string itemName)
        {
            foreach (PrefabSlot prefab in prefabs)
            {
                if (prefab.itemName == itemName)
                    return prefab.gameObject;
            }
            Debug.LogWarning("Prefab with itemName " + itemName + " was not found in storage");
            return null;
        }
    }
}
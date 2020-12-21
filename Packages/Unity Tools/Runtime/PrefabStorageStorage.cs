using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools 
{
    [CreateAssetMenu(fileName = "Prefab Storage Storage", menuName = "ScriptableObjects/Prefab Storage Storage")]
    public class PrefabStorageStorage : ScriptableObject
    {
        public PrefabStorage[] prefabStorages;

        public GameObject GetPrefab(string itemName)
        {
            foreach(PrefabStorage prefabStorage in prefabStorages)
            {
                foreach (PrefabSlot prefab in prefabStorage.prefabs)
                {
                    if (prefab.itemName == itemName)
                        return prefab.gameObject;
                }
            }
            Debug.LogWarning("Prefab with itemName " + itemName + " was not found in storage storage");
            return null;
        }
    }
}
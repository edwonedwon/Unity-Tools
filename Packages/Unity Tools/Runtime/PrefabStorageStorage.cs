using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.UnityTools 
{
    [CreateAssetMenu(fileName = "Prefab Storage Storage", menuName = "ScriptableObjects/Prefab Storage Storage")]
    public class PrefabStorageStorage : ScriptableObject
    {
        public PrefabStorage[] prefabStorages;

        public GameObject GetPrefab(string prefabName)
        {
            foreach(PrefabStorage prefabStorage in prefabStorages)
            {
                foreach (GameObject prefab in prefabStorage.prefabs)
                {
                    if (prefab.name == prefabName)
                        return prefab;
                }
            }
            Debug.LogWarning("Prefab with name " + prefabName + " was not found in storage storage");
            return null;
        }
    }
}
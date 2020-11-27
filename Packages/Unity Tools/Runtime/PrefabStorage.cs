using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.UnityTools 
{
    [CreateAssetMenu(fileName = "Prefab Storage", menuName = "ScriptableObjects/Prefab Storage")]
    public class PrefabStorage : ScriptableObject
    {
        public GameObject[] prefabs;

        public GameObject GetPrefab(string prefabName)
        {
            foreach (GameObject prefab in prefabs)
            {
                if (prefab.name == prefabName)
                    return prefab;
            }
            Debug.LogWarning("Prefab with name " + prefabName + " was not found in storage");
            return null;
        }
    }
}
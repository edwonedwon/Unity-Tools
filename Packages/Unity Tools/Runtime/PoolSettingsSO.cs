using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pool Settings", menuName = "Scriptables/Pool Settings")]
public class PoolSettingsSO : ScriptableObject
{
    [System.Serializable]
    public struct Prefab
    {
        public GameObject prefab;
        public string poolableType;
        public int numberInPool;
    }

    public List<Prefab> prefabs;
}

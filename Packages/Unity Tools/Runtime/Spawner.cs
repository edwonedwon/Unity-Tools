using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Edwon.Tools
{
    public class Spawner : MonoBehaviour
    {
        public bool debugLog;
        public bool spawnOnAwake = false;
        [Header("used to filter events from SpawnEventSender")]
        public new string name;
        [Header("set spawn point here")]
        public Transform spawnPoint;
        [Header("if null can be passed in by event")]
        public GameObject prefabToSpawn;
        [Header("prefab storage is how SpawnDuplicate() finds the same prefab as this")]
        public PrefabStorageStorage prefabStorageToSearch;

        void Awake()
        {
            if (name.IsWhiteSpaceOnly())
                name = gameObject.name;

            if (spawnPoint == null)
                spawnPoint = transform;
            
            if (spawnOnAwake)
                SpawnSet();
        }

        public GameObject SpawnGiven(GameObject toSpawn)
        {
            if (debugLog)
                Debug.Log("SpawnGiven: " + toSpawn.name);
            return Spawn(toSpawn);
        }

        public void SpawnSet()
        {
            if (debugLog)
                Debug.Log("SpawnSet: " + prefabToSpawn.name);
            Spawn(prefabToSpawn);
        }

        public void SpawnDuplicate()
        {
            string prefabName = Utils.RemoveParenthesisAndInside(gameObject.name);
            GameObject prefabFromStorage = prefabStorageToSearch.GetPrefab(prefabName);
            if (prefabFromStorage != null)
            {
                if (debugLog)
                    Debug.Log("SpawnDuplicate: " + prefabFromStorage.name);
                Spawn(prefabFromStorage);        
            }
        }

        public void SpawnAndHoldDuplicate()
        {
            string prefabName = Utils.RemoveParenthesisAndInside(gameObject.name);
            GameObject prefabFromStorage = prefabStorageToSearch.GetPrefab(prefabName);
            if (prefabFromStorage != null)
            {
                GameObject spawned = Spawn(prefabFromStorage);
                IHoldable toHold = spawned.GetComponent<IHoldable>();
                Holder holder = spawnPoint.GetComponent<Holder>();
                holder.Hold(toHold);
                if (debugLog)
                    Debug.Log("SpawnAndHoldDuplicate: " + prefabFromStorage.name);
            }
            else
            {
                Debug.LogWarning("couldn't find prefab in storage with name " + prefabName);
            }
        }

        public void SpawnAndHoldDuplicateAfter(float delay)
        {
            StaticCoroutine.DoAfter(delay, ()=> SpawnAndHoldDuplicate());
        }

        GameObject Spawn(GameObject toSpawn)
        {
            GameObject spawned = GameObject.Instantiate(toSpawn, spawnPoint.position, spawnPoint.rotation);
            string name = toSpawn.name;
            string nameStripped = Utils.RemoveParenthesisAndInside(name);
            int totalObjectsWithSameName = 1 + Utils.HowManyOtherGameObjectsWithSameName(name, true);
            spawned.name = nameStripped + " (" + totalObjectsWithSameName + ")";
            return spawned;
        }

        void OnSpawnEvent(string spawnerName, GameObject prefabToSpawn)
        {
            if (name == spawnerName)
            {
                Spawn(prefabToSpawn);
            }
        }

        void OnEnable() 
        {
            SpawnEventSender.spawnEvent += OnSpawnEvent;
        }

        void OnDisable()
        {
            SpawnEventSender.spawnEvent -= OnSpawnEvent;
        }
    }
}
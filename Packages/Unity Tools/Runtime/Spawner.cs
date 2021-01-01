using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Edwon.Tools
{
    public class Spawner : MonoBehaviour
    {
        public bool debugLog;
        public enum SpawnOnAwakeOption { Dont, Spawn, SpawnAndHold };
        public SpawnOnAwakeOption spawnOnAwake = SpawnOnAwakeOption.Dont;
        public Transform spawnTransform;
        public Holder holder; // only needed of spawn and hold is called
        public GameObject prefabToSpawn;
        public PrefabStorageStorageSO allPrefabs;

        void Awake()
        {
            if (name.IsWhiteSpaceOnly())
                name = gameObject.name;

            if (holder == null)
                holder = GetComponent<Holder>();

            if (spawnTransform == null)
                spawnTransform = transform;
            
            switch (spawnOnAwake)
            {
                case SpawnOnAwakeOption.Spawn:
                    SpawnSet();
                    break;
                case SpawnOnAwakeOption.SpawnAndHold:
                    SpawnAndHoldSet();
                    break;
            }
        }

        GameObject Spawn(GameObject toSpawn)
        {
            GameObject spawned = GameObject.Instantiate(toSpawn, spawnTransform.position, spawnTransform.rotation);
            return spawned;
        }

        public void SpawnSet()
        {
            if (debugLog)
                Debug.Log("SpawnSet: " + prefabToSpawn.name);
            Spawn(prefabToSpawn);
        }

        public GameObject SpawnGiven(GameObject toSpawn)
        {
            if (debugLog)
                Debug.Log("SpawnGiven: " + toSpawn.name);
            return Spawn(toSpawn);
        }

        public void SpawnAndHoldSet()
        {
            SpawnAndHold(prefabToSpawn);
        }

        public void SpawnAndHoldPrefab(string prefabName)
        {
            if (debugLog)
                Debug.Log("spawn and hold " + prefabName);
            GameObject prefab = allPrefabs.GetPrefab(prefabName);
            if (holder == null) { Debug.Log("prefab with name: " + prefabName + " is not in given prefab storage"); return; }
            SpawnAndHold(prefab);            
        }

        public void SpawnAndHold(GameObject holdableToSpawn)
        {
            if (holder == null) { Debug.Log("holder is not set on Spawner " + name); return; }

            GameObject spawned = SpawnGiven(holdableToSpawn);
            holder.ReleaseAndHold(spawned);
        }

        public void SpawnAndHoldAndDestroyHeld(GameObject holdableToSpawn)
        {
            if (holder == null) { Debug.Log("holder is not set on Spawner " + name); return; }

            holder.DestroyHeld();
            GameObject spawned = SpawnGiven(holdableToSpawn);
            holder.ReleaseAndHold(spawned);
        }
    }
}
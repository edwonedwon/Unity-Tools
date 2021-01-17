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
        public ItemStorageSO itemStorage;
        public ItemPoolSO itemPool;

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
            return GameObject.Instantiate(toSpawn, spawnTransform.position, spawnTransform.rotation);
        }

        GameObject Spawn(string itemName)
        {
            return itemPool.SpawnFromPool(itemName).gameObject;
        }

        public void SpawnSet()
        {
            if (debugLog)
                Debug.Log("SpawnSet: " + prefabToSpawn.name);
            Spawn(prefabToSpawn);
        }

        public void SpawnAndHoldSet()
        {
            SpawnAndHold(prefabToSpawn);
        }

        public void SpawnAndHold(GameObject holdableToSpawn)
        {
            if (holder == null) { Debug.Log("holder is not set on Spawner " + name); return; }

            GameObject spawned = Spawn(holdableToSpawn);
            holder.ReleaseAndHold(spawned);
        }

        public void SpawnAndHold(string itemName)
        {
            if (debugLog){ Debug.Log("spawn and hold " + itemName); }
            GameObject spawned = Spawn(itemName);
            if (holder == null) { Debug.Log("prefab with name: " + itemName + " is not in given prefab storage"); return; }
            SpawnAndHold(spawned);            
        }
        
        public void SpawnAndHoldAndDestroyHeld(string itemName)
        {
            if (holder == null) { Debug.Log("holder is not set on Spawner " + name); return; }
            holder.DestroyHeld();
            SpawnAndHold(itemName);
        }

        public void SpawnAndHoldAndDestroyHeld(GameObject holdableToSpawn)
        {
            if (holder == null) { Debug.Log("holder is not set on Spawner " + name); return; }

            holder.DestroyHeld();
            SpawnAndHold(holdableToSpawn);
        }

    }
}
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

        GameObject Spawn(GameObject prefab)
        {
            return GameObject.Instantiate(prefab, spawnTransform.position, spawnTransform.rotation);
        }

        GameObject Spawn(string itemName)
        {
            Item spawned = null;
            spawned = itemPool.SpawnFromPool(itemName, spawnTransform.position, spawnTransform.rotation);
            if (spawned != null)
                return spawned.gameObject;
            else
                return null;
        }

        public void SpawnSet()
        {
            if (debugLog){ Debug.Log("SpawnSet: " + prefabToSpawn.name); }
            Spawn(prefabToSpawn);
        }

        public void SpawnAndHoldSet()
        {
            if (debugLog){ Debug.Log("SpawnAndHoldSet: " + prefabToSpawn.name); }
            SpawnAndHold(prefabToSpawn);
        }

        public void SpawnAndHold(GameObject prefab)
        {
            if (debugLog){ Debug.Log("SpawnAndHold " + prefab.name); }
            if (holder == null) { Debug.Log("holder is not set on Spawner " + name); return; }

            GameObject spawned = Spawn(prefab);
            holder.ReleaseAndHold(spawned);
        }

        public void SpawnAndHold(string itemName)
        {
            if (debugLog){ Debug.Log("SpawnAndHold " + itemName); }
            if (holder == null) { Debug.Log("hold is not set on Spawner " + name); return; }
            GameObject spawned = Spawn(itemName);
            if (spawned != null)
                holder.ReleaseAndHold(spawned);
        }
        
        public void SpawnAndHoldAndDestroyHeld(string itemName)
        {
            if (debugLog){ Debug.Log("SpawnAndHoldAndDestroyHeld: " + itemName); }
            if (holder == null) { Debug.Log("holder is not set on Spawner " + name); return; }
            holder.DestroyHeld();
            SpawnAndHold(itemName);
        }

        public void SpawnAndHoldAndDestroyHeld(GameObject holdableToSpawn)
        {
            if (debugLog){ Debug.Log("SpawnAndHoldAndDestroyHeld: " + holdableToSpawn.name); }
            if (holder == null) { Debug.Log("holder is not set on Spawner " + name); return; }

            holder.DestroyHeld();
            SpawnAndHold(holdableToSpawn);
        }

    }
}
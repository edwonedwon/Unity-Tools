using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class ItemPoolSpawner : MonoBehaviour
    {
        public bool debugLog;
        public enum SpawnOnAwakeOption { Dont, Spawn, SpawnAndHold };
        public SpawnOnAwakeOption spawnOnAwake = SpawnOnAwakeOption.Dont;
        public Transform spawnPoint;
        public Holder holder; // only needed of spawn and hold is called
        public string setItemToSpawn;
        public ItemPoolSO itemPool;

        void Awake()
        {
            if (name.IsWhiteSpaceOnly())
                name = gameObject.name;

            if (holder == null)
                holder = GetComponent<Holder>();

            if (spawnPoint == null)
                spawnPoint = transform;
            
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

        GameObject Spawn(string itemName)
        {
            Item spawned = null;
            spawned = itemPool.SpawnFromPool(itemName, spawnPoint.position, spawnPoint.rotation);
            if (spawned != null)
                return spawned.gameObject;
            else
                return null;
        }

        public void SpawnSet()
        {
            if (debugLog){ Debug.Log("SpawnSet: " + setItemToSpawn); }
            Spawn(setItemToSpawn);
        }

        public void SpawnAndHoldSet()
        {
            if (debugLog){ Debug.Log("SpawnAndHoldSet: " + setItemToSpawn); }
            SpawnAndHold(setItemToSpawn);
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
    }
}
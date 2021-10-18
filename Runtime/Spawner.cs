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
        public Transform spawnPoint;
        public Holder holder; // only needed of spawn and hold is called
        public GameObject prefabToSpawn;
        public ItemStorageSO itemStorage;

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

        GameObject Spawn(GameObject prefab)
        {
            return GameObject.Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
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

        public void SpawnAndHoldAndDestroyHeld(GameObject holdableToSpawn)
        {
            if (debugLog){ Debug.Log("SpawnAndHoldAndDestroyHeld: " + holdableToSpawn.name); }
            if (holder == null) { Debug.Log("holder is not set on Spawner " + name); return; }

            holder.DestroyHeld();
            SpawnAndHold(holdableToSpawn);
        }

    }
}
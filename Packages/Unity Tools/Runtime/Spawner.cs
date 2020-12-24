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
        [Header("used to filter events from SpawnEventSender")]
        public new string name;
        [Header("set spawn point here")]
        public Transform spawnPoint;
        [Header("only needed if SpawnAndHold is called")]
        public Holder holder;
        [Header("if null can be passed in by event")]
        public GameObject prefabToSpawn;
        [Header("prefab storage with all game prefabs")]
        public PrefabStorageStorageSO allPrefabs;

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

        GameObject Spawn(GameObject toSpawn)
        {
            GameObject spawned = GameObject.Instantiate(toSpawn, spawnPoint.position, spawnPoint.rotation);
            string name = toSpawn.name;
            string nameStripped = Utils.RemoveParenthesisAndInside(name);
            int totalObjectsWithSameName = 1 + Utils.HowManyOtherGameObjectsWithSameName(name, true);
            spawned.name = nameStripped + " (" + totalObjectsWithSameName + ")";
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

        public void SpawnAndHoldPrefab(string itemName)
        {
            Debug.Log("spawn and hold " + itemName);
            GameObject prefab = allPrefabs.GetPrefab(itemName);
            if (holder == null) { Debug.Log("prefab with name: " + itemName + " is not in given prefab storage"); return; }
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

        void OnSpawnEvent(string spawnerName, GameObject prefabToSpawn)
        {
            if (name == spawnerName)
                Spawn(prefabToSpawn);
        }

        void OnSpawnAndHoldEvent(string spawnerName, GameObject prefabToSpawn)
        {
            if (name == spawnerName)
                SpawnAndHold(prefabToSpawn);
        }

        void OnEnable() 
        {
            SpawnEventSender.spawnEvent += OnSpawnEvent;
            SpawnEventSender.spawnAndHoldEvent += OnSpawnAndHoldEvent;
        }

        void OnDisable()
        {
            SpawnEventSender.spawnEvent -= OnSpawnEvent;
            SpawnEventSender.spawnAndHoldEvent -= OnSpawnAndHoldEvent;
        }
    }
}
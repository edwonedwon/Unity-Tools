using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class SpawnEventSender : MonoBehaviour
    {
        public delegate void SpawnEvent(string spawnerName, GameObject prefab);
        public static SpawnEvent spawnEvent;
        public delegate void SpawnAndHoldEvent(string spawnerName, GameObject prefab);
        public static SpawnAndHoldEvent spawnAndHoldEvent;
        // public delegate void SpawnAndHoldDuplicateEvent(string spawnerName);
        // public static SpawnAndHoldDuplicateEvent spawnAndHoldDuplicateEvent;
        public GameObject prefabToSpawn;
        public string spawnerName;
        PrefabReference prefabReference;

        void Awake()
        {
            if (prefabReference == null)
                prefabReference = GetComponent<PrefabReference>();
        }

        [InspectorButton("SendSpawnEvent")]
        public bool sendSpawnEvent;
        public void SendSpawnEvent()
        {
            if (spawnEvent == null)
                Debug.Log("spawnEvent is null, no listeners subscribed");
            else
                spawnEvent(spawnerName, prefabToSpawn);
        }

        [InspectorButton("SendSpawnAndHoldEvent")]
        public bool sendSpawnAndHoldEvent;
        public void SendSpawnAndHoldEvent()
        {
            if (spawnAndHoldEvent == null)
                Debug.Log("spawnEvent is null, no listeners subscribed");
            else
                spawnAndHoldEvent(spawnerName, prefabToSpawn);
        }

        [InspectorButton("SendSpawnAndHoldDuplicateEvent")]
        public bool sendSpawnAndHoldDuplicateEvent;
        public void SendSpawnAndHoldDuplicateEvent()
        {
            if (spawnAndHoldEvent == null)
                Debug.Log("spawnEvent is null, no listeners subscribed");
            else if (prefabReference == null)
                Debug.Log("PrefabReference component is missing, cannot spawn duplicate");
            else
                spawnAndHoldEvent(spawnerName, prefabReference.Prefab);
        }
    }
}
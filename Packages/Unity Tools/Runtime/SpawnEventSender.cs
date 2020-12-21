using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    }
}
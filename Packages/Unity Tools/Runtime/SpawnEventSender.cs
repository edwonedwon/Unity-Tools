using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEventSender : MonoBehaviour
{
    public delegate void SpawnEvent(string spawnerName, GameObject prefab);
    public static SpawnEvent spawnEvent;
    public GameObject prefabToSpawn;
    public string spawnerName;

    [InspectorButton("SendSpawnEvent")]
    public bool sendSpawnEvent;
    public void SendSpawnEvent()
    {
        spawnEvent(spawnerName, prefabToSpawn);
    }
}

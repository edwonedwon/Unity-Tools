using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEventSender : MonoBehaviour
{
    public delegate void SpawnEvent(string spawnerName, GameObject prefab);
    public static SpawnEvent spawnEvent;
    public delegate void SpawnAndHoldEvent(string spawnerName, GameObject prefab);
    public static SpawnAndHoldEvent spawnAndHoldEvent;
    public GameObject prefabToSpawn;
    public string spawnerName;

    [InspectorButton("SendSpawnEvent")]
    public bool sendSpawnEvent;
    public void SendSpawnEvent()
    {
        if (spawnEvent == null)
            Debug.LogWarning("spawnEvent is null, no listeners subscribed");
        else
            spawnEvent(spawnerName, prefabToSpawn);
    }

    [InspectorButton("SendSpawnAndHoldEvent")]
    public bool sendSpawnAndHoldEvent;
    public void SendSpawnAndHoldEvent()
    {
        if (spawnAndHoldEvent == null)
            Debug.LogWarning("spawnEvent is null, no listeners subscribed");
        else
            spawnAndHoldEvent(spawnerName, prefabToSpawn);
    }
}

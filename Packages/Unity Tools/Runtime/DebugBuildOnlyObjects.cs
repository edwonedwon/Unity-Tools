using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBuildOnlyObjects : MonoBehaviour
{
    public GameObject[] debugObjects;

    void Awake()
    {
        if (Application.isEditor)
            return;
            
        foreach(GameObject debugObject in debugObjects)
        {
            if (Debug.isDebugBuild)
                debugObject.SetActive(true);
            else
                debugObject.SetActive(false);
        }
    }
}

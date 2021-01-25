using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    public bool debugLog = true;

    public void DebugLog(string message)
    {
        if (debugLog)
            Debug.Log(message);
    }
}

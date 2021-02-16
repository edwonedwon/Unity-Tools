using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    public bool debugLog = true;
    public string extraMessage;

    public void DebugLog(string message)
    {
        if (debugLog)
            Debug.Log(message);
    }

    public void DebugLog(int i)
    {
        if (debugLog)
            Debug.Log(extraMessage + " " + i);
    }

    public void DebugLog(float f)
    {
        if (debugLog)
            Debug.Log(extraMessage + " " + f);
    }
}

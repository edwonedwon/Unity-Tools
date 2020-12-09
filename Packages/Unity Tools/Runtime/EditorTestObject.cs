using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorTestObject : MonoBehaviour
{
    void Awake()
    {
        #if !UNITY_EDITOR
        gameObject.SetActive(false);
        #endif
    }
}

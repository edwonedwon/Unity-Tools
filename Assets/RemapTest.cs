using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.Tools;

public class RemapTest : MonoBehaviour
{
    public int testValue = 5;
    public int fromMin = 0;
    public int fromMax = 10;
    public int toMin = 0;
    public int toMax = 100;

    [InspectorButton("RemapTestMethod")]
    public bool remapTestMethod;
    public void RemapTestMethod()
    {
        int log = testValue.Remap(fromMin, fromMax, toMin, toMax);
        Debug.Log(log);
    }
}
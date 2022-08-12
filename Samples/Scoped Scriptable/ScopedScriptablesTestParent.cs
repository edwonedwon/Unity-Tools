using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.Tools;
using Sirenix.OdinInspector;

public class ScopedScriptablesTestParent : MonoBehaviour, IScopedScriptableUser
{
    [InlineEditor]
    public VariableBool testBool;
    ScopedScriptablesEnforcer enforcer;

    public GameObject childPrefab;

    [Button]
    void SpawnChild()
    {
        GameObject.Instantiate(childPrefab, transform);
    }

    [Button]
    void DestroyChild()
    {
        if (transform.GetChild(0) != null)
            GameObject.Destroy(transform.GetChild(0).gameObject);
    }

    public List<ScopedScriptable> GetScopedScriptables()
    {
        return new List<ScopedScriptable>
        {
            testBool,
        };
    }

    public void SetScopedScriptables(ScopedScriptablesEnforcer enforcer)
    {
        testBool = enforcer.GetInstance<VariableBool>(testBool);
    }
}

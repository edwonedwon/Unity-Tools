using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.Tools;
using Sirenix.OdinInspector;

public class ScopedScriptablesTestChild : MonoBehaviour, IScopedScriptableUser
{
    [InlineEditor]
    public VariableBool testBool;
    ScopedScriptablesEnforcer enforcer;

    void Awake()
    {
        enforcer = GetComponentInParent<ScopedScriptablesEnforcer>();
        enforcer.RegisterScopedScriptableUser(gameObject);
    }

    void OnDestroy()
    {
        enforcer.UnregisterScopedScriptableUser(this);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.Tools;

public class ScopedScriptableUserExample : MonoBehaviour, IScopedScriptableUser
{
    public VariableFloat stat;

    public List<ScopedScriptable> GetScopedScriptables()
    {
        return new List<ScopedScriptable>()
        {
            stat,
        };
    }

    public void SetScopedScriptables(ScopedScriptablesEnforcer enforcer)
    {
        stat = enforcer.GetInstance<VariableFloat>(stat);
    }
}
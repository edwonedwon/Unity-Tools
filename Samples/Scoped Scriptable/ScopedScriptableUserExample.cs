using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.Tools;

public class ScopedScriptableUserExample : MonoBehaviour, IUniqueScriptableUser
{
    public VariableFloat stat;

    public List<ScopedScriptable> GetUniqueScriptables()
    {
        return new List<ScopedScriptable>()
        {
            stat,
        };
    }

    public void SetUniqueScriptables(ScopedScriptablesEnforcer enforcer)
    {
        stat = enforcer.GetInstance<VariableFloat> stat;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.Tools;

public class UniqueScriptableUserExample : MonoBehaviour, IUniqueScriptableUser
{
    public VariableFloat stat;

    public List<UniqueScriptable> GetUniqueScriptables()
    {
        List<UniqueScriptable> originals = new List<UniqueScriptable>();
        originals.Add(stat);
        return originals;
    }

    public void SetUniqueScriptables(List<UniqueScriptableInstance> instances)
    {
        stat = instances.Find(x => x.asset == stat).instance as VariableFloat;
    }
}

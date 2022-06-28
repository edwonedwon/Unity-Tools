using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// creates a scriptable that is a unique instance
// the instance will then make sure that this gameObject
// is using the same instance across all components that implement IUniqueScriptableUser
public class UniqueScriptable : ScriptableObject
{
    public void CreateUniqueInstance(GameObject owner)
    {
        UniqueScriptable original = this;
        UniqueScriptable instance = Instantiate(original);

        // send message to users of this scriptable to replace the original with the instance
        List<IUniqueScriptableUser> users = owner.GetComponents<IUniqueScriptableUser>().ToList();
        foreach(IUniqueScriptableUser user in users)
            user.ReplaceScriptablesWithUnique(original, instance);
    }
}

// an interface to 
public interface IUniqueScriptableUser
{
    void CreateUniqueScriptableInstances();
    public void ReplaceScriptablesWithUnique(UniqueScriptable original, UniqueScriptable instance);
}
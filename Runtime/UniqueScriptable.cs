using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Edwon.Tools
{
    // creates a scriptable that is a unique instance
    // the instance will then make sure that this gameObject
    // is using the same instance across all components that implement IUniqueScriptableUser
    public class UniqueScriptable : ScriptableObject
    {
        
    }

    // an interface to 
    public interface IUniqueScriptableUser
    {
        public List<UniqueScriptable> GetUniqueScriptableOriginals();
        public void ReplaceUniqueScriptables(UniqueScriptable original, UniqueScriptable instance);
    }
}
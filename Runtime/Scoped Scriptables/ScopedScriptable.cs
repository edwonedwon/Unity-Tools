using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Edwon.Tools
{
    public class ScopedScriptable : ScriptableObject
    {
        [NonSerialized]
        public bool isInstance;
    }

    // an interface to 
    public interface IScopedScriptableUser
    {
        public List<ScopedScriptable> GetScopedScriptables();
        public void SetScopedScriptables(List<ScopedScriptableInstance> instances);
    }
}
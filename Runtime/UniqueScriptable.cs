using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Edwon.Tools
{
    public class UniqueScriptable : ScriptableObject
    {
        
    }

    // an interface to 
    public interface IUniqueScriptableUser
    {
        public List<UniqueScriptable> GetUniqueScriptables();
        public void SetUniqueScriptables(UniqueScriptable original, UniqueScriptable instance);
    }
}
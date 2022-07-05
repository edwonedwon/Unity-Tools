using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Edwon.Tools
{
    // a scriptable object that can be "scoped" to one game object
    // this is useful for having shared variables between components on a game object
    // each scoped scriptable reference is made unique by the ScopedSciptablesEnforcer component
    public class ScopedScriptable : ScriptableObject
    {
        GameObject owner;
        [ReadOnly]
        public ScopedScriptable instance;

        public delegate void RegisterScopedScriptableEvent(GameObject owner, ScopedScriptable asset);
        public static event RegisterScopedScriptableEvent registerScopedScriptable;

        public virtual void InitScopedScriptable(GameObject owner)
        {
            this.owner = owner;
            if (registerScopedScriptable != null)
                registerScopedScriptable(owner, this);
        }

        void OnSetScopedScriptableInstance(GameObject owner, List<ScopedScriptableInstance> instances)
        {
            if (owner == this.owner)
            {
                foreach(var instance in instances)
                {
                    if (instance.asset == this)
                    {
                        this.instance = instance.instance;
                        break;
                    }
                }
            }
        }

        void OnEnable()
        {
            ScopedScriptablesEnforcer.onSetScopedScriptableInstance += OnSetScopedScriptableInstance;
        }

        void OnDisable()
        {
            ScopedScriptablesEnforcer.onSetScopedScriptableInstance -= OnSetScopedScriptableInstance;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Edwon.Tools
{
    public class VariableTransformSetter : MonoBehaviour, IScopedScriptableUser
    {
        [InlineEditor]
        public VariableTransform variableTransform;
        public bool useAsScopedScriptable = true;
        public bool onAwakeSetToThis = false;
        public bool onEnableSetToThis = true;

        void OnAwake()
        {
            if (onAwakeSetToThis)
                variableTransform.RuntimeValue = transform;
        }

        void OnEnable()
        {
            if (onEnableSetToThis)
                variableTransform.RuntimeValue = transform;
        }

        public List<ScopedScriptable> GetScopedScriptables() 
        {
            if (useAsScopedScriptable)
                return new List<ScopedScriptable>() { variableTransform };
            else
                return new List<ScopedScriptable>();
        }

        public void SetScopedScriptables(ScopedScriptablesEnforcer enforcer) 
        { 
            if (useAsScopedScriptable)
                variableTransform = enforcer.GetInstance<VariableTransform>(variableTransform);
        }
    }
}

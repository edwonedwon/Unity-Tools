using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.Tools
{
    public class VariableSOListener<VariableSOType, VariableType, UnityEventType> 
    where VariableSOType: VariableSO<VariableType>
    where UnityEventType : UnityEventEdwonBase<VariableType>
    {
        public VariableSOType variableSO;
        public VariableType variableLast;
        public UnityEventType onVariableChanged;

        public void Awake()
        {
            if (variableSO == null)
            {
                return;
            }
            
            variableLast = variableSO.runtimeValue;
            onVariableChanged.Invoke(variableSO.runtimeValue);
        }

        public VariableSOListener(VariableSOType variableSO, UnityEventType onVariableChanged)
        {
            if (variableSO == null)
                return;

            variableLast = variableSO.runtimeValue;
            this.variableSO = variableSO;
            this.onVariableChanged = onVariableChanged;
        }

        public void Update() 
        {
            if (variableSO == null)
                return;
                
            if (!variableSO.runtimeValue.Equals(variableLast))
                onVariableChanged.Invoke(variableSO.runtimeValue);

            variableLast = variableSO.runtimeValue;
        }
    }
}
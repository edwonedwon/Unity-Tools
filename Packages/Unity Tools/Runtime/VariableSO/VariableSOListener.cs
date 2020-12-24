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
        VariableType variableLast;
        public UnityEventType onVariableChanged;

        public VariableSOListener(VariableSOType variableSO, UnityEventType onVariableChanged)
        {
            this.variableSO = variableSO;
            this.onVariableChanged = onVariableChanged;
        }

        public void Update() 
        {
            if (!variableSO.runtimeValue.Equals(variableLast))
                onVariableChanged.Invoke(variableSO.runtimeValue);

            variableLast = variableSO.runtimeValue;
        }
    }
}
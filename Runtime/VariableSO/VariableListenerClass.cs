using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.Tools
{
    public class VariableListenerClass<VariableSOType, VariableType, UnityEventType> 
    where VariableSOType: Variable<VariableType>
    where UnityEventType : UnityEventEdwonBase<VariableType>
    {
        public VariableSOType variableSO;
        public VariableType variableLast;
        public UnityEventType onVariableChanged;

        public VariableListenerClass(VariableSOType variableSO, UnityEventType onVariableChanged)
        {
            if (variableSO == null)
                return;

            variableLast = variableSO.RuntimeValue;
            this.variableSO = variableSO;
            this.onVariableChanged = onVariableChanged;
            onVariableChanged.Invoke(variableSO.RuntimeValue);
        }

        public void Update() 
        {
            if (variableSO == null)
                return;
                
            if (!variableSO.RuntimeValue.Equals(variableLast))
                onVariableChanged.Invoke(variableSO.RuntimeValue);

            variableLast = variableSO.RuntimeValue;
        }
    }
}
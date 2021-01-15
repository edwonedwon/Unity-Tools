using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.Tools
{
    public class IntVariableSOListenerClass
    {
        public IntVariableSO variableSO;
        public int variableLast;
        public UnityEventInt onVariableChanged;
        public UnityEventInt onVariableIncreased;
        public UnityEventInt onVariableChangedAboveZero;
        public UnityEvent onVariableZero;

        public IntVariableSOListenerClass(
            IntVariableSO variableSO, 
            UnityEventInt onVariableChanged,
            UnityEventInt onVariableIncreased,
            UnityEventInt onVariableChangedAboveZero,
            UnityEvent onVariableZero)
        {
            if (variableSO == null)
                return;

            variableLast = variableSO.runtimeValue;
            this.variableSO = variableSO;
            this.onVariableChanged = onVariableChanged;
            this.onVariableIncreased = onVariableIncreased;
            this.onVariableChangedAboveZero = onVariableChangedAboveZero;
            this.onVariableZero = onVariableZero;
            onVariableChanged.Invoke(variableSO.runtimeValue);
            ZeroOrNotZeroEvents();
        }

        public void Update() 
        {
            if (variableSO == null)
                return;
                
            if (!variableSO.runtimeValue.Equals(variableLast))
                onVariableChanged.Invoke(variableSO.runtimeValue);

            variableLast = variableSO.runtimeValue;

            if (variableSO.runtimeValue != variableLast)
            {
                ZeroOrNotZeroEvents();
                if (variableSO.runtimeValue > variableLast)
                    onVariableIncreased.Invoke(variableSO.runtimeValue);
            }
        }
        
        public void ZeroOrNotZeroEvents()
        {
            if (variableSO == null)
                return;

            if (variableSO.runtimeValue == 0) 
                onVariableZero.Invoke();
            if (variableSO.runtimeValue > 0)
                onVariableChangedAboveZero.Invoke(variableSO.runtimeValue);
        }
    }
}
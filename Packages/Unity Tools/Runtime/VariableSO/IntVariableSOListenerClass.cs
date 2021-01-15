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
        public bool debugLog = false;

        public IntVariableSOListenerClass(
            IntVariableSO variableSO, 
            UnityEventInt onVariableChanged,
            UnityEventInt onVariableIncreased,
            UnityEventInt onVariableChangedAboveZero,
            UnityEvent onVariableZero)
        {
            if (variableSO == null)
                return;

            this.variableSO = variableSO;
            this.onVariableChanged = onVariableChanged;
            this.onVariableIncreased = onVariableIncreased;
            this.onVariableChangedAboveZero = onVariableChangedAboveZero;
            this.onVariableZero = onVariableZero;
            Init();
        }

        public IntVariableSOListenerClass(IntVariableSO variableSO, UnityEventInt onVariableChangedAboveZero)
        {
            this.variableSO = variableSO;
            this.onVariableChangedAboveZero = onVariableChangedAboveZero;
            Init();
        }

        void Init()
        {
            variableLast = variableSO.runtimeValue;
            if (onVariableChanged != null)
            {
                onVariableChanged.Invoke(variableSO.runtimeValue);
                if (debugLog)
                    Debug.Log("INIT variabled changed to " + variableSO.runtimeValue);
            }
            ZeroOrNotZeroEvents();
        }

        public void Update() 
        {
            if (variableSO == null)
                return;
                
            if (onVariableChanged != null)
                if (!variableSO.runtimeValue.Equals(variableLast))
                {
                    if (debugLog)
                        Debug.Log("variabled changed to " + variableSO.runtimeValue);
                    onVariableChanged.Invoke(variableSO.runtimeValue);
                }

            variableLast = variableSO.runtimeValue;

            if (variableSO.runtimeValue != variableLast)
            {
                ZeroOrNotZeroEvents();
                if (onVariableIncreased != null)
                    if (variableSO.runtimeValue > variableLast)
                        onVariableIncreased.Invoke(variableSO.runtimeValue);
            }
        }
        
        public void ZeroOrNotZeroEvents()
        {
            if (variableSO == null)
                return;

            if (onVariableZero != null)
                if (variableSO.runtimeValue == 0) 
                    onVariableZero.Invoke();

            if (onVariableChangedAboveZero != null)
                if (variableSO.runtimeValue > 0)
                    onVariableChangedAboveZero.Invoke(variableSO.runtimeValue);
        }
    }
}
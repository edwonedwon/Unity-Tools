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

        public IntVariableSOListenerClass(IntVariableSO variableSO)
        {
            if (variableSO == null)
            {
                Debug.Log("variableSO is null");
                return;
            }
            this.variableSO = variableSO;
            onVariableChanged = new UnityEventInt();
            onVariableIncreased = new UnityEventInt();
            onVariableChangedAboveZero = new UnityEventInt();
            onVariableZero = new UnityEvent();
            Init();
        }

        public IntVariableSOListenerClass(
            IntVariableSO variableSO, 
            ref UnityEventInt onVariableChanged,
            ref UnityEventInt onVariableIncreased,
            ref UnityEventInt onVariableChangedAboveZero,
            ref UnityEvent onVariableZero)
        {
            if (variableSO == null)
            {
                Debug.Log("variableSO is null");
                return;
            }
            this.variableSO = variableSO;
            this.onVariableChanged = onVariableChanged;
            this.onVariableIncreased = onVariableIncreased;
            this.onVariableChangedAboveZero = onVariableChangedAboveZero;
            this.onVariableZero = onVariableZero;
            Init();
        }

        void Init()
        {
            variableLast = variableSO.runtimeValue;
            onVariableChanged.Invoke(variableSO.runtimeValue);
            if (debugLog)
                Debug.Log("INIT variabled changed to " + variableSO.runtimeValue);
            ZeroOrNotZeroEvents();
        }

        public void Update() 
        {
            if (variableSO == null)
                return;
                
            if (!variableSO.runtimeValue.Equals(variableLast))
            {
                if (debugLog)
                    Debug.Log("variabled changed to " + variableSO.runtimeValue);
                onVariableChanged.Invoke(variableSO.runtimeValue);
                ZeroOrNotZeroEvents();
                if (variableSO.runtimeValue > variableLast)
                {
                    if (debugLog)
                        Debug.Log("variabled changed to " + variableSO.runtimeValue);
                    onVariableIncreased.Invoke(variableSO.runtimeValue);
                }
            }

            variableLast = variableSO.runtimeValue;
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

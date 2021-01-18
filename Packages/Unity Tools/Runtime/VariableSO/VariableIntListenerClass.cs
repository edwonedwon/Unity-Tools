using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.Tools
{
    public class VariableIntListenerClass
    {
        public VariableInt variable;
        public int variableLast;
        public UnityEventInt onVariableChanged;
        public UnityEventInt onVariableIncreased;
        public UnityEventInt onVariableChangedAboveZero;
        public UnityEvent onVariableZero;
        public bool debugLog = false;

        public VariableIntListenerClass(VariableInt variable)
        {
            if (variable == null)
            {
                Debug.Log("variable is null");
                return;
            }
            this.variable = variable;
            onVariableChanged = new UnityEventInt();
            onVariableIncreased = new UnityEventInt();
            onVariableChangedAboveZero = new UnityEventInt();
            onVariableZero = new UnityEvent();
            Init();
        }

        public VariableIntListenerClass(
            VariableInt variable, 
            ref UnityEventInt onVariableChanged,
            ref UnityEventInt onVariableIncreased,
            ref UnityEventInt onVariableChangedAboveZero,
            ref UnityEvent onVariableZero)
        {
            if (variable == null)
            {
                Debug.Log("variable is null");
                return;
            }
            this.variable = variable;
            this.onVariableChanged = onVariableChanged;
            this.onVariableIncreased = onVariableIncreased;
            this.onVariableChangedAboveZero = onVariableChangedAboveZero;
            this.onVariableZero = onVariableZero;
            Init();
        }

        void Init()
        {
            variableLast = variable.runtimeValue;
            onVariableChanged.Invoke(variable.runtimeValue);
            if (debugLog)
                Debug.Log("INIT variabled changed to " + variable.runtimeValue);
            ZeroOrNotZeroEvents();
        }

        public void Update() 
        {
            if (variable == null)
                return;
                
            if (!variable.runtimeValue.Equals(variableLast))
            {
                if (debugLog)
                    Debug.Log("variabled changed to " + variable.runtimeValue);
                onVariableChanged.Invoke(variable.runtimeValue);
                ZeroOrNotZeroEvents();
                if (variable.runtimeValue > variableLast)
                {
                    if (debugLog)
                        Debug.Log("variabled changed to " + variable.runtimeValue);
                    onVariableIncreased.Invoke(variable.runtimeValue);
                }
            }

            variableLast = variable.runtimeValue;
        }
        
        public void ZeroOrNotZeroEvents()
        {
            if (variable == null)
                return;

            if (variable.runtimeValue == 0) 
                onVariableZero.Invoke();

            if (variable.runtimeValue > 0)
                onVariableChangedAboveZero.Invoke(variable.runtimeValue);
        }
    }
}

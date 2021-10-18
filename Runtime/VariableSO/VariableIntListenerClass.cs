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
            variableLast = variable.RuntimeValue;
            onVariableChanged.Invoke(variable.RuntimeValue);
            if (debugLog)
                Debug.Log("INIT variabled changed to " + variable.RuntimeValue);
            ZeroOrNotZeroEvents();
        }

        public void Update() 
        {
            if (variable == null)
                return;
                
            if (!variable.RuntimeValue.Equals(variableLast))
            {
                if (debugLog)
                    Debug.Log("variabled changed to " + variable.RuntimeValue);
                onVariableChanged.Invoke(variable.RuntimeValue);
                ZeroOrNotZeroEvents();
                if (variable.RuntimeValue > variableLast)
                {
                    if (debugLog)
                        Debug.Log("variabled changed to " + variable.RuntimeValue);
                    onVariableIncreased.Invoke(variable.RuntimeValue);
                }
            }

            variableLast = variable.RuntimeValue;
        }
        
        public void ZeroOrNotZeroEvents()
        {
            if (variable == null)
                return;

            if (variable.RuntimeValue == 0) 
                onVariableZero.Invoke();

            if (variable.RuntimeValue > 0)
                onVariableChangedAboveZero.Invoke(variable.RuntimeValue);
        }
    }
}

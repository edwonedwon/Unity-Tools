using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Edwon.Tools
{
    public class IntVariableSOListener : MonoBehaviour
    {
        public IntVariableSO variableSO;
        public UnityEventInt onVariableChanged;
        public UnityEventInt onVariableIncreased;
        public UnityEventInt onVariableChangedAboveZero;
        public UnityEvent onVariableZero;
        VariableSOListener<IntVariableSO, int, UnityEventInt> listener;

        private void Awake() 
        {
            listener = new VariableSOListener<IntVariableSO, int, UnityEventInt>(variableSO, onVariableChanged);
            listener.Awake();

            ZeroOrNotZeroEvents();
        }

        private void Update() 
        {
            if (variableSO == null)
                return;
                
            listener.Update();   
            
            if (variableSO.runtimeValue != listener.variableLast)
            {
                ZeroOrNotZeroEvents();
                if (variableSO.runtimeValue > listener.variableLast)
                {
                    onVariableIncreased.Invoke(variableSO.runtimeValue);
                }
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
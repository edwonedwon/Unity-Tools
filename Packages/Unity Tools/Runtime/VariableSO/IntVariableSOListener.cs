using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Edwon.Tools
{
    public class IntVariableSOListener : MonoBehaviour
    {
        [SerializeField]
        [ReadOnly]
        IntVariableSO variableSO;
        public UnityEventInt onVariableChanged;
        public UnityEventInt onVariableIncreased;
        public UnityEventInt onVariableChangedAboveZero;
        public UnityEvent onVariableZero;
        IntVariableSOListenerClass listener;

        private void Awake() 
        {
            if (variableSO == null)
                return;
                
            listener = new IntVariableSOListenerClass(
                variableSO, 
                onVariableChanged, 
                onVariableIncreased, 
                onVariableChangedAboveZero, 
                onVariableZero);
        }

        private void Update() 
        {
            if (variableSO == null)
                return;
                
            listener.Update();   
        }

        public void SetVariableSO(IntVariableSO variableSo)
        {
            this.variableSO = variableSo;
            listener = new IntVariableSOListenerClass(
                variableSO, 
                onVariableChanged, 
                onVariableIncreased, 
                onVariableChangedAboveZero, 
                onVariableZero);
        }


    }
}
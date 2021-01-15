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
        public bool debugLog = false;

        private void Awake() 
        {
            if (variableSO == null)
                return;
                
            InitListener();
        }

        void InitListener()
        {
            listener = new IntVariableSOListenerClass(
                variableSO, 
                ref onVariableChanged, 
                ref onVariableIncreased, 
                ref onVariableChangedAboveZero, 
                ref onVariableZero);
            listener.debugLog = debugLog;
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
            InitListener();
        }


    }
}
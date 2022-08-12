using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Edwon.Tools
{
    public class VariableIntListener : MonoBehaviour
    {
        [SerializeField]
        VariableInt variableSO;
        public UnityEventInt onVariableChanged;
        public UnityEventInt onVariableIncreased;
        public UnityEventInt onVariableChangedAboveZero;
        public UnityEvent onVariableZero;
        VariableIntListenerClass listener;
        public bool debugLog = false;

        private void Awake() 
        {
            if (variableSO == null)
                return;
                
            InitListener();
        }

        void InitListener()
        {
            if (variableSO == null)
                Debug.Log("variableSO is null on VariableIntListener on: " + gameObject.name);
            listener = new VariableIntListenerClass(
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

        public void SetVariableSO(VariableInt variableSo)
        {
            this.variableSO = variableSo;
            InitListener();
        }


    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class IntVariableListener : MonoBehaviour
    {
        public IntVariableSO variableSO;
        public UnityEventInt onVariableChanged;
        VariableListener<IntVariableSO, int, UnityEventInt> listener;

        private void Awake() 
        {
            listener = new VariableListener<IntVariableSO, int, UnityEventInt>(variableSO, onVariableChanged);
        }

        private void Update() 
        {
            listener.Update();    
        }
    }
}
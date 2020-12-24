using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class IntVariableSOListener : MonoBehaviour
    {
        public IntVariableSO variableSO;
        public UnityEventInt onVariableChanged;
        VariableSOListener<IntVariableSO, int, UnityEventInt> listener;

        private void Awake() 
        {
            listener = new VariableSOListener<IntVariableSO, int, UnityEventInt>(variableSO, onVariableChanged);
        }

        private void Update() 
        {
            listener.Update();    
        }
    }
}
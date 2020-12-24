using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class FloatVariableListener : MonoBehaviour 
    {
        public FloatVariableSO variableSO;
        public UnityEventFloat onVariableChanged;
        VariableListener<FloatVariableSO, float, UnityEventFloat> listener;

        private void Awake() 
        {
            listener = new VariableListener<FloatVariableSO, float, UnityEventFloat>(variableSO, onVariableChanged);
        }

        private void Update() 
        {
            listener.Update();    
        }
    }
}
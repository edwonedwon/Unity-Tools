using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class FloatVariableSOListener : MonoBehaviour 
    {
        public FloatVariableSO variableSO;
        public UnityEventFloat onVariableChanged;
        VariableSOListenerClass<FloatVariableSO, float, UnityEventFloat> listener;

        private void Awake() 
        {
            listener = new VariableSOListenerClass<FloatVariableSO, float, UnityEventFloat>(variableSO, onVariableChanged);
        }

        private void Update() 
        {
            listener.Update();    
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class VariableFloatListener : MonoBehaviour 
    {
        public VariableFloat variableSO;
        public UnityEventFloat onVariableChanged;
        VariableListenerClass<VariableFloat, float, UnityEventFloat> listener;

        private void Awake() 
        {
            listener = new VariableListenerClass<VariableFloat, float, UnityEventFloat>(variableSO, onVariableChanged);
        }

        private void Update() 
        {
            listener.Update();    
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Edwon.Tools
{
    public class VariableFloatListener : MonoBehaviour 
    {
        [InlineEditor]
        public VariableFloat variableSO;
        public UnityEventFloat onAwake;
        public UnityEventFloat onVariableChanged;
        VariableListenerClass<VariableFloat, float, UnityEventFloat> listener;

        private void Awake() 
        {
            onAwake.Invoke(variableSO.RuntimeValue);
            listener = new VariableListenerClass<VariableFloat, float, UnityEventFloat>(variableSO, onVariableChanged);
        }

        private void Update() 
        {
            listener.Update();
        }
    }
}
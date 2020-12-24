using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class FloatVariableListener : MonoBehaviour
    {
        public FloatVariableSO floatVariable;
        float floatLast;
        public UnityEventFloat onFloatVariableChanged;

        void Update() 
        {
            if (floatVariable.runtimeValue != floatLast)
                onFloatVariableChanged.Invoke(floatVariable.runtimeValue);

            floatLast = floatVariable.runtimeValue;
        }
    }
}
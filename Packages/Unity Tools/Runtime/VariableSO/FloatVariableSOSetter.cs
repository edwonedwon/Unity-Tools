using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class FloatVariableSOSetter : MonoBehaviour
    {
        public FloatVariableSO floatVariableSO;

        public void SetTo(float value)
        {
            floatVariableSO.runtimeValue = value;
        }

        [InspectorButton("PlusOne")]
        public bool plusOne;
        public void PlusOne()
        {
            floatVariableSO.runtimeValue += 1;
        }

        [InspectorButton("MinusOne")]
        public bool minusOne;
        public void MinusOne()
        {
            floatVariableSO.runtimeValue -= 1;
        }

        [InspectorButton("DebugSetTo")]
        public bool debugSetTo;
        public void DebugSetTo()
        {
            SetTo(debugSetToValue);
        }
        public float debugSetToValue;

    }
}
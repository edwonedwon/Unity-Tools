using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edwon.Tools
{
    public class VariableFloatSetter : MonoBehaviour
    {
        public VariableFloat floatVariableSO;

        public void SetTo(float value)
        {
            floatVariableSO.RuntimeValue = value;
        }

        [InspectorButton("PlusOne")]
        public bool plusOne;
        public void PlusOne()
        {
            floatVariableSO.RuntimeValue += 1;
        }

        [InspectorButton("MinusOne")]
        public bool minusOne;
        public void MinusOne()
        {
            floatVariableSO.RuntimeValue -= 1;
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